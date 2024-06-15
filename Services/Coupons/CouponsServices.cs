using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.DTOs;
using BaobabBackEndService.Repository.Coupons;
using BaobabBackEndService.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BaobabBackEndService.Services.Coupons
{
    public class CouponsServices : ICouponsServices
    {
        private readonly ICouponsRepository _couponsRepository;
        private readonly IMapper _mapper;


        public CouponsServices(ICouponsRepository couponsRepository, IMapper mapper)
        {
            _couponsRepository = couponsRepository;
            _mapper = mapper;
        }

        /*
        Parte 2:

        En esta sección, implementamos toda la lógica del negocio. Este servicio actúa como intermediario entre el `CouponsRepository` y el controlador.

        - La lógica del negocio se define aquí, separando las responsabilidades de acceso a datos y manejo de solicitudes HTTP.
        - Este servicio toma las solicitudes del controlador, aplica cualquier lógica necesaria y luego interactúa con el `CouponsRepository` para acceder a los datos.

        El objetivo es mantener el código organizado y adherirse al principio de separación de responsabilidades, asegurando que cada componente del sistema tenga una única responsabilidad bien definida.

        Para ver mas consulta el archivo Service/Coupons/CouponsRepository.
        Continuemos con el siguiente paso Alli en este archivo...
        */

        // ----------------------- GET COUPON & CATEGORY:
        public async Task<IEnumerable<Coupon>> GetCouponAndCategory()
        {
            return await _couponsRepository.GetCouponAndCategoyAsync();
        }

        public IEnumerable<Coupon> GetCoupons()
        {
            // Lógica de negocio para obtene rtodos los cupones

            return _couponsRepository.GetCoupons();
        }

        public async Task<Coupon> GetCoupon(string id)
        {
            if (int.TryParse(id, out int couponId))
            {
                return null;
            }
            return await _couponsRepository.GetCouponAsync(couponId);
        }
        public async Task<ResponseUtils<Coupon>> GetCouponsAsync(string searchType, string value)
        {
            try
            {
                if (!int.TryParse(searchType, out int parseSearchType))
                {
                    return new ResponseUtils<Coupon>(false,null,400, message: "Dato ingresado no es valido.");
                }

                List<Coupon> coupons;

                switch (parseSearchType)
                {
                    case 1:
                        if (!int.TryParse(value, out int couponId))
                        {
                            return new ResponseUtils<Coupon>(false,null,404, message: "No se encontraron coincidencias en la base de datos.");
                        }
                        coupons = new List<Coupon>(await _couponsRepository.GetCouponByIdAsync(couponId));
                        break;
                    case 2:
                        coupons = new List<Coupon>(await _couponsRepository.GetCouponByTitleSearchAsync(value));
                        break;
                    case 3:
                        coupons = new List<Coupon>(await _couponsRepository.GetCouponByCouponCodeSearchAsync(value));
                        break;
                    default:
                        return new ResponseUtils<Coupon>(false, null, 404, message: "Dato ingresado no es válido.");
                }

                if (coupons == null || !coupons.Any())
                {
                    return new ResponseUtils<Coupon>(false, null, 404, message: "No se encontraron cupones con los criterios de búsqueda proporcionados.");
                }

                return new ResponseUtils<Coupon>(true, coupons,200, message: "Se encontraron los cupones correctamente.");

            }
            catch (Exception ex)
            {
                return new ResponseUtils<Coupon>(false, null,500, message: "Error buscar el cupon en la base de datos: " + ex.InnerException.Message);
            }
        }

        public async Task<ResponseUtils<Coupon>> CreateCoupon(CouponDTO request)
        {
            var coupon = _mapper.Map<Coupon>(request);//se pasa el dato al mapper y ya

            var existingCodeCoupon = await _couponsRepository.GetCouponByCouponCodeAsync(coupon.CouponCode);
            var existingTitleCoupon = await _couponsRepository.GetCouponByTitleAsync(coupon.Title);
            if (existingCodeCoupon != null)
            {
                return new ResponseUtils<Coupon>(false,null,409, message: "El codigo Del cupon ya existe");
            }
            if (existingTitleCoupon != null)
            {
                return new ResponseUtils<Coupon>(false,null,409, message: "El Titulo Del cupon ya existe");
            }

            coupon.CreationDate = DateTime.Now;
            coupon.StatusCoupon = "Creado";

            return new ResponseUtils<Coupon>(true, new List<Coupon> { _couponsRepository.CreateCoupon(coupon) }, 201, message: "Cupon exitosamente creado!");
        }
        // ----------------------- EDIT ACTION:
        public async Task<ResponseUtils<Coupon>> EditCoupon(int marketingUserId, Coupon coupon)
        {
            try
            {
                // Se confirma si el cupón existe en la tabla 'MassiveCoupons':
                var existCoupon = await _couponsRepository.GetMassiveCouponByCouponId(coupon);
                // Condicional que determina si se ha encontrado el cupón:
                if (existCoupon == null)
                {
                    // Se actualiza la entidad 'Coupons' en la base de datos:
                    await _couponsRepository.UpdateCoupon(coupon);
                    // Se crea una instancia del modelo 'ChangeHistory' con la información requerida para crear un nuevo registro en la entidad:
                    var newChange = new ChangeHistory
                    {
                        ModifiedTable = "Coupons",
                        IdModifiedRecord = coupon.Id,
                        ChangeDate = DateTime.Now,
                        IdMarketingUser = marketingUserId
                    };
                    // Se crea un nuevo registro en la entidad 'ChangesHistory':
                    await _couponsRepository.AddNewChange(newChange);
                    // Retorno de la respuesta éxitosa con la estructura de la clase 'ResponseUtils':
                    return new ResponseUtils<Coupon>(true, new List<Coupon> { coupon }, 200, message: "¡Cupón actualizado!");
                }
                else
                {
                    return new ResponseUtils<Coupon>(false, null, 406, message: "¡El cupón ya fue redimido, no es posible actualizarlo!");
                }
            }
            catch (Exception ex)
            {
                return new ResponseUtils<Coupon>(false, null, 500, message: $"Error interno del servidor: {ex.Message}");
            }
        }
        // Postman body para hacer testing:
        // {
        //     "Id" : 4,
        //     "Title": "Hallloween Editado",
        //     "Description" : "Get 30% off on your purchases for Halloween.",
        //     "CreationDate" : "2024-05-14T21:12:51",
        //     "StartDate" : "2024-05-14T21:12:51",
        //     "ExpiryDate" : "2024-05-27T21:12:51",
        //     "ValueDiscount" : 30,
        //     "TypeDiscount": "Porcentual",
        //     "NumberOfAvailableUses" : 50,
        //     "TypeUsability": "Limitada",
        //     "StatusCoupon": "Activo",
        //     "MinPurchaseRange": 500000,
        //     "MaxPurchaseRange": 10000000,
        //     "CouponCode": "HALLOWEENEDIT",
        //     "CategoryId" : 1,
        //     "MarketingUserId" : 3
        // }

        // ----------------------- VALIDATE ACTION:
        public async Task<ResponseUtils<Coupon>> ValidateCoupon(string couponCode, float purchaseValue)
        {
            try
            {
                // Se inicializa variable confirmando si el cupón existe en la base de datos:
                var existCoupon = await _couponsRepository.GetCouponByCouponCodeAsync(couponCode);
                // Se confirma si se ha encontrado el cupón:
                if (existCoupon != null)
                {
                    // Se confirma si el estado del cupón es 'Activo':
                    if (existCoupon.StatusCoupon == "Activo")
                    {
                        // Se confirma el tipo de usabilidad del cupón:
                        if (existCoupon.TypeUsability == "Limitada")
                        {
                            // Se confirma si el cupón tiene usos disponibles:
                            if (existCoupon.NumberOfAvailableUses > 0)
                            {
                                // Se confirma la fecha de expiración del cupón:
                                var currentDate = DateTime.Now;
                                if (existCoupon.ExpiryDate >= currentDate)
                                {
                                    // Se confirma el tipo de cupón 'Porcentual' o 'Neto':
                                    if (existCoupon.TypeDiscount == "Porcentual")
                                    {
                                        // Se confirma el rango del valor comprado:
                                        if (purchaseValue >= existCoupon.MinPurchaseRange && purchaseValue <= existCoupon.MaxPurchaseRange)
                                        {
                                            // Retorno de la respuesta éxitosa con la estructura de la clase 'ResponseUtils':
                                            return new ResponseUtils<Coupon>(true, new List<Coupon> { existCoupon }, 200, message: "¡Cupón válido!");
                                        }
                                        else
                                        {
                                            return new ResponseUtils<Coupon>(false, null, 409, message: "¡El rango de la compra no cumple los requerimientos!");
                                        }
                                    }
                                    else
                                    {
                                        // Se confirma el rango del valor comprado:
                                        if (purchaseValue >= existCoupon.MinPurchaseRange)
                                        {
                                            // Retorno de la respuesta éxitosa con la estructura de la clase 'ResponseUtils':
                                            return new ResponseUtils<Coupon>(true, new List<Coupon> { existCoupon }, 200, message: "¡Cupón válido!");
                                        }
                                        else
                                        {
                                            return new ResponseUtils<Coupon>(false, null, 409, message: "¡El rango de la compra no cumple los requerimientos!");
                                        }
                                    }
                                }
                                else
                                {
                                    return new ResponseUtils<Coupon>(false, null, 409, message: "¡El cupón ha expirado!");
                                }
                            }
                            else
                            {
                                return new ResponseUtils<Coupon>(false, null, 409, message: "Cupón sin usos disponibles!");
                            }
                        }
                        else
                        {
                            // Se confirma la fecha de expiración del cupón:
                            var currentDate = DateTime.Now;
                            if (existCoupon.ExpiryDate >= currentDate)
                            {
                                // Se confirma el tipo de cupón 'Porcentual' o 'Neto':
                                if (existCoupon.TypeDiscount == "Porcentual")
                                {
                                    // Se confirma el rango del valor comprado:
                                    if (purchaseValue >= existCoupon.MinPurchaseRange && purchaseValue <= existCoupon.MaxPurchaseRange)
                                    {
                                        // Retorno de la respuesta éxitosa con la estructura de la clase 'ResponseUtils':
                                        return new ResponseUtils<Coupon>(true, new List<Coupon> { existCoupon }, 200, message: "¡Cupón válido!");
                                    }
                                    else
                                    {
                                        return new ResponseUtils<Coupon>(false, null, 409, message: "¡El rango de la compra no cumple los requerimientos!");
                                    }
                                }
                                else
                                {
                                    // Se confirma el rango del valor comprado:
                                    if (purchaseValue >= existCoupon.MinPurchaseRange)
                                    {
                                        // Retorno de la respuesta éxitosa con la estructura de la clase 'ResponseUtils':
                                        return new ResponseUtils<Coupon>(true, new List<Coupon> { existCoupon }, 200, message: "¡Cupón válido!");
                                    }
                                    else
                                    {
                                        return new ResponseUtils<Coupon>(false, null, 409, message: "¡El rango de la compra no cumple los requerimientos!");
                                    }
                                }
                            }
                            else
                            {
                                return new ResponseUtils<Coupon>(false, null, 409, message: "¡El cupón ha expirado!");
                            }
                        }
                    }
                    else
                    {
                        return new ResponseUtils<Coupon>(false, null, 409, message: "¡Cupón no activo!");
                    }
                }
                else
                {
                    return new ResponseUtils<Coupon>(false, null, 404, message: "¡Cupón no encontrado!");
                }
            }
            catch (Exception ex)
            {
                return new ResponseUtils<Coupon>(false, null, 400, $"Error: {ex.Message}");
            }
        }

        //funcion para buscar, Filtrar o mostrar cuponen
        public async Task<ResponseUtils<Coupon>> FilterSearch(string Search)
        {
            
            var capitalized = Search.Substring(0,1).ToUpper() + Search.Substring(1);
            var Cupones = await _couponsRepository.GetCouponsAsync();

            if (capitalized == "Activo" || capitalized == "Inactivo" || capitalized == "Creado" || capitalized == "Vencido" || capitalized == "Agotado")
            {

                Cupones = Cupones.Where(x => x.StatusCoupon == capitalized).ToList();
                return new ResponseUtils<Coupon>(true, new List<Coupon>(Cupones), 200, message: "Se ha encotrado la informacion");

            }
            else
            {
                //buscador
                if (!string.IsNullOrEmpty(capitalized))
                {
                    Cupones = Cupones.Where(x => x.CouponCode.ToLower() == capitalized.ToLower()).ToList();
                    if (!Cupones.Any())
                    {
                        return new ResponseUtils<Coupon>(false, null, 404, message: "El cupon no fue encontrado");
                    }
                }
            }
            return new ResponseUtils<Coupon>(true, new List<Coupon>(Cupones), 200, message: "Cupones encontrados correctamente");
        }

        public async Task<ResponseUtils<Coupon>> EditCouponStatus(string id, string status)
        {

            if (!int.TryParse(id, out int couponId) || !int.TryParse(status, out int statusNum))
            {
                return new ResponseUtils<Coupon>(false, null, 400, message: "El parametro ingresado no es valido");
            }
            var coupon = await _couponsRepository.GetCouponAsync(couponId);
            if (coupon == null)
            {
                return new ResponseUtils<Coupon>(false,null,404, message: "El cupon no fue encontrado");
            }

            switch (statusNum)
            {
                case 1:
                    coupon.StatusCoupon = "Activo";
                    break;
                case 2:
                    coupon.StatusCoupon = "Inactivo";
                    break;
                case 3:
                    coupon.StatusCoupon = "Vencido";
                    break;
                case 4:
                    coupon.StatusCoupon = "Agotado";
                    break;
                case 5:
                    coupon.StatusCoupon = "Creado";
                    break;
                case 6:
                    coupon.StatusCoupon = "Eliminado";
                    break;

                default:
                    return new ResponseUtils<Coupon>(false,null,400, message: "El parametro ingresado no es valido");
            }
            try
            {
                await _couponsRepository.UpdateStatusCouponAsync(coupon);
                return new ResponseUtils<Coupon>(true, null, 200, message: "Cupon editado exitosamente");
            }
            catch (Exception ex)
            {
                return new ResponseUtils<Coupon>(false, null, 500,message: "Error buscar el cupon en la base de datos: " + ex.InnerException.Message);
            }
        }        //redencion de cupon
        public async Task<ResponseUtils<MassiveCoupon>> RedeemCoupon(RedeemDTO redeemRequest)
        {
            var validate = true;

            if (/* validate.Status */ validate == true)
            {
                var CuponValido = _couponsRepository.CuponCode(redeemRequest.CodeCoupon);

                //validar si el cupon es null
                if (CuponValido == null)
                {
                    return new ResponseUtils<MassiveCoupon>(false,null,404, message: "El cupon no existe en la base de datos");
                }

                //cambiar estado a agotado
                if (CuponValido.NumberOfAvailableUses == 0)
                {
                    CuponValido.StatusCoupon = "Agotado";
                    await _couponsRepository.RedencionCupon(CuponValido);
                    return new ResponseUtils<MassiveCoupon>(false,null,409, message: "El cupon esta Agotado");
                }

                //Cambiar estado a Vencido
                if (DateTime.Now > CuponValido.ExpiryDate)
                {
                    CuponValido.StatusCoupon = "Vencido";
                    await _couponsRepository.RedencionCupon(CuponValido);
                    return new ResponseUtils<MassiveCoupon>(false, null,409,message: "El cupon esta Vencido");
                }


                if (CuponValido.TypeUsability == "Limitada" && CuponValido.NumberOfAvailableUses > 0)
                {
                    CuponValido.NumberOfAvailableUses = CuponValido.NumberOfAvailableUses - 1;
                    await _couponsRepository.RedencionCupon(CuponValido);

                }

                MassiveCoupon massiveCoupon = _mapper.Map<MassiveCoupon>(redeemRequest);
                massiveCoupon.CouponId = CuponValido.Id;

                var CreatePoll = await _couponsRepository.CrearPoll(massiveCoupon);
                return new ResponseUtils<MassiveCoupon>(true, new List<MassiveCoupon> { CreatePoll }, 200, message: "El cupon fue redimido con exito");

            }
            else
            {
                return new ResponseUtils<MassiveCoupon>(false,null,400, message: "El cupon no es valido");
            }

        }

    }
}