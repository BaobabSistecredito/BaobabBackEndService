-- CREAR TABLA:
CREATE TABLE Coupons (
    Id INT NOT NULL AUTO_INCREMENT UNIQUE PRIMARY KEY,
    Title VARCHAR(255),
    Description TEXT,
    CreationDate DATETIME,
    StartDate DATETIME,
    ExpiryDate DATETIME,
    ValueDiscount FLOAT,
    TypeDiscount ENUM("Porcentual", "Neto"),
    NumberOfAvailableUses INT,
    TypeUsability ENUM("Ilimitada", "Limitada"),
    StatusCoupon ENUM("Activo", "Inactivo", "Vencido", "Agotado", "Creado"),
    MinPurchaseRange FLOAT,
    MaxPurchaseRange FLOAT,
    CouponCode VARCHAR(255),
    CategoryId INT,
    MarketingUserId INT,
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id),
    FOREIGN KEY (MarketingUserId) REFERENCES MarketingUsers(Id)
);

-- EDITAR CAMPO 'StatusCoupon':
ALTER TABLE Coupons MODIFY COLUMN StatusCoupon ENUM("Activo", "Inactivo", "Vencido", "Agotado", "Creado", "Eliminado");

-- INSERTAR DATOS:
INSERT INTO Coupons (
    Title, 
    Description, 
    CreationDate, 
    StartDate, 
    ExpiryDate, 
    ValueDiscount, 
    TypeDiscount, 
    NumberOfAvailableUses, 
    TypeUsability, 
    StatusCoupon, 
    MinPurchaseRange, 
    MaxPurchaseRange, 
    CouponCode, 
    CategoryId, 
    MarketingUserId
) VALUES (
    'Christmas', 
    'Get $200.000 off on your purchases for Christmas.', 
    NOW(), 
    NOW(), 
    DATE_ADD(NOW(), INTERVAL 2 WEEK), 
    200000, 
    'Neto', 
    0, 
    'Ilimitada', 
    'Creado', 
    500000, 
    10000000, 
    'CHRISTMAS', 
    1, 
    2
),
(
    'Hallloween', 
    'Get 30% off on your purchases for Halloween.', 
    NOW(), 
    NOW(), 
    DATE_ADD(NOW(), INTERVAL 1 WEEK), 
    30, 
    'Porcentual', 
    100, 
    'Limitada', 
    'Creado', 
    500000, 
    10000000, 
    'CHRISTMAS', 
    1, 
    3
);

-- ELIMINAR TABLA:
DROP TABLE Coupons;

-- ---------------------------------------------------------------------------------

-- CREAR TABLA:
CREATE TABLE MassiveCoupons(
    Id INT NOT NULL AUTO_INCREMENT UNIQUE PRIMARY KEY,
    MassiveCouponCode VARCHAR(255) UNIQUE,
    CouponId INT,
    UserEmail VARCHAR(125) UNIQUE,
    PurchaseId VARCHAR(125) UNIQUE,
    RedemptionDate DATETIME,
    PurchaseValue FLOAT,
    FOREIGN KEY (CouponId) REFERENCES Coupons(Id)
);

-- INSERTAR DATOS:
INSERT INTO MassiveCoupons (
    MassiveCouponCode, 
    CouponId, 
    UserEmail, 
    PurchaseId,
    RedemptionDate, 
    PurchaseValue
) VALUES (
    'B2gF4dE12HQ4e',
    3, 
    'test@gmail.com',
    '6',
    NOW(), 
    300000
);

-- ELIMINAR TABLA:
DROP TABLE MassiveCoupons;

-- ---------------------------------------------------------------------------------

-- CREAR TABLA:
CREATE TABLE Categories(
    Id INT NOT NULL AUTO_INCREMENT UNIQUE PRIMARY KEY,
    CategoryName VARCHAR(255),
    Status ENUM("Activo", "Inactivo")
);

-- INSERTAR DATOS:
INSERT INTO Categories (
    CategoryName,
    Status
) VALUES (
    'Navidad', 
    'Inactivo'
),
( 
    'Halloween',
    'Activo'
);

-- ELIMINAR TABLA:
DROP TABLE Categories;

-- ---------------------------------------------------------------------------------

-- CREAR TABLA:
CREATE TABLE MarketingUsers(
    Id INT AUTO_INCREMENT UNIQUE NOT NULL PRIMARY KEY,
    UserName VARCHAR(255),
    Password VARCHAR(255),
    EmployeeId INT,
    Uuid CHAR(36)
);

-- INSERTAR DATOS:
INSERT INTO MarketingUsers (UserName, Password, EmployeeId) VALUES 
('pedro_pablo', '123', 1),
('jaimito', '123', 2),
('user', '123', 3);
-- INSERTAR DATOS:
INSERT INTO MarketingUsers (UserName, Password, EmployeeId) VALUES 
('pedrooooo', '123', 4),
('jaimitoooooo', '123', 5),
('userrrrr', '123', 6);

-- ELIMINAR TABLA:
DROP TABLE MarketingUsers;

-- ELIMINAR COLUMNA:
ALTER TABLE MarketingUsers DROP COLUMN Uuid;

-- ---------------------------------------------------------------------------------

-- CREAR TABLA:
CREATE TABLE ChangesHistory(
    Id INT AUTO_INCREMENT UNIQUE NOT NULL PRIMARY KEY,
    ModifiedTable ENUM("Coupons", "Categories"),
    IdModifiedTable INT,
    ChangeDate DATETIME,
    IdMarketingUser INT,
    FOREIGN KEY (IdMarketingUser) REFERENCES MarketingUsers(Id)
);

-- MODIFICAR NOMBRE DE UN CAMPO:
ALTER TABLE ChangesHistory CHANGE COLUMN IdModifiedTable IdModifiedRecord INT;

-- ELIMINAR TABLA:
DROP TABLE ChangesHistory;

-- ---------------------------------------------------------------------------------
-- TRIGGERS:
-- DELIMITER //

-- CREATE TRIGGER GenerateUUID 
-- BEFORE INSERT ON MarketingUsers
-- FOR EACH ROW
-- BEGIN
--     SET NEW.Uuid = UUID();
-- END//

-- DELIMITER ;