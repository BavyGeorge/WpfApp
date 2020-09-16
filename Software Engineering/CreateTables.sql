CREATE PROC CreateTables
AS
DROP TABLE IF EXISTS [Login]
DROP TABLE IF EXISTS UserGoals
DROP TABLE IF EXISTS UserDiary
DROP TABLE IF EXISTS Users
DROP TABLE IF EXISTS [DATA.FT]
DROP TABLE IF EXISTS [NRF.FT]
DROP TABLE IF EXISTS [CODE.FT]
DROP TABLE IF EXISTS [CSM.FT]
DROP TABLE IF EXISTS [YF.FT]
DROP TABLE IF EXISTS [INGREDIENT.FT]
DROP TABLE IF EXISTS [DATA.AP]
DROP TABLE IF EXISTS [NAME.FT]

CREATE TABLE Users (
UserID INT IDENTITY,
Firstname VARCHAR (50) NOT NULL,
LASTNAME VARCHAR (50) NOT NULL,
GENDER VARCHAR (20) NOT NULL,
DOB DATE NOT NULL,
Height INT NOT NULL,
[Weight] INT NOT NULL,
Ethnicity VARCHAR (50) NOT NULL,
Medical VARCHAR (250) NOT NULL,
CONSTRAINT User_pk PRIMARY KEY (UserID)
)

SET IDENTITY_INSERT Users ON;

INSERT INTO Users (UserID, Firstname, LASTNAME, GENDER, DOB, Height, [Weight], Ethnicity, Medical) VALUES (
1, 'admin' , 'admin' , 'N/A' , CAST(GETDATE() AS DATE) , 0 , 0 , 'N/A' , 'N/A');

SET IDENTITY_INSERT Users OFF;

CREATE TABLE [Login] (
UserID INT,
Username NVARCHAR (50) UNIQUE,
[Password] NVARCHAR (50)
CONSTRAINT User_fk FOREIGN KEY (UserID) REFERENCES Users(UserID)
CONSTRAINT Login_pk PRIMARY KEY (UserID)
)

INSERT INTO [Login] VALUES (
1, 'admin', 'admin');

CREATE TABLE UserGoals (
UserID INT,
Energy DECIMAL(18,2),
PROTEIN DECIMAL(18,2),
[Fat, Total] DECIMAL(18,2),
[Fat, Saturated] DECIMAL(18,2),
Carbohydrates DECIMAL(18,2),
Sugars DECIMAL(18,2),
[Dietary Fibre] DECIMAL(18,2),
Sodium DECIMAL(18,2),
CONSTRAINT UserID_fk FOREIGN KEY (UserID) REFERENCES Users(UserID),
CONSTRAINT UserGoals_pk PRIMARY KEY (UserID)
)

CREATE TABLE [NAME.FT] (
FoodID VARCHAR(50) NOT NULL,
[Food Name] VARCHAR(MAX),
[Short Food Name] VARCHAR(MAX),
[Alternative Name] VARCHAR(MAX),
[Food Description] VARCHAR(MAX),
[Food Item Generic Name] VARCHAR(MAX),
[Food Item Kind] VARCHAR(MAX),
[Food Item Part] VARCHAR(MAX),
[Process/State] NVARCHAR(MAX),
[Brand/Strain] NVARCHAR(MAX),
Grade VARCHAR(MAX),
Maturity VARCHAR(MAX),
Genus VARCHAR(MAX),
Species VARCHAR(MAX),
Variety VARCHAR(MAX),
[Sampling details] VARCHAR(MAX),
[Component message] VARCHAR(MAX)
CONSTRAINT [Name.FT_pk] PRIMARY KEY (FoodID)
)

CREATE TABLE [DATA.FT] (
FoodID VARCHAR(50),
[Component Identifier] VARCHAR(50),
[Value] DECIMAL(18,2),
[Source] VARCHAR(MAX)
CONSTRAINT FoodID_fk FOREIGN KEY (FoodID) REFERENCES [NAME.FT](FoodID),
CONSTRAINT [Data.FT_pk] PRIMARY KEY (FoodID, [Component Identifier])
)

CREATE TABLE [INGREDIENT.FT] (
RecipeID VARCHAR(50),
IngredientID VARCHAR(50),
[Ingredient Name] VARCHAR(MAX),
[Weight Fraction] DECIMAL(18,2),
[Nutrient Retention Factor ID] VARCHAR(20),
[USDA Retention factor description] VARCHAR(MAX)
CONSTRAINT RecipeID_fk FOREIGN KEY (RecipeID) REFERENCES [NAME.FT](FoodID),
CONSTRAINT IngredientID_fk FOREIGN KEY (IngredientID) REFERENCES [NAME.FT](FoodID),
CONSTRAINT Ingredient_pk PRIMARY KEY (RecipeID, IngredientID, [Weight Fraction])
)

CREATE TABLE [DATA.AP] (
FoodID VARCHAR(50),
Chapter VARCHAR(MAX),
[Food Name] VARCHAR(MAX),
Alcohol DECIMAL(18,2),
[Alpha-carotene] DECIMAL(18,2),
[Alpha-tocopherol] DECIMAL(18,2),
Ash DECIMAL(18,2),
[Available carbohydrate by difference] DECIMAL(18,2),
[Available carbohydrate, FSANZ] DECIMAL(18,2),
[Available carbohydrates by weight] DECIMAL(18,2),
[Available carbohydrates in monosaccharide equivalent] DECIMAL(18,2),
[Beta-carotene] DECIMAL(18,2),
[Beta-carotene equivalents] DECIMAL(18,2),
[Beta-tocopherol] DECIMAL(18,2),
Caffeine DECIMAL(18,2),
Calcium DECIMAL(18,2),
[Carbohydrate by difference, FSANZ] DECIMAL(18,2),
Cholesterol DECIMAL(18,2),
Copper DECIMAL(18,2),
[Delta-tocopherol] DECIMAL(18,2),
[Dietary folate equivalents] DECIMAL(18,2),
[Dry matter] DECIMAL(18,2),
[Edible portion] DECIMAL(18,2),
[Energy, total metabolisable (kcal)] DECIMAL(18,2),
[Energy, total metabolisable (kcal, including dietary fibre)] DECIMAL(18,2),
[Energy, total metabolisable (kJ)] DECIMAL(18,2),
[Energy, total metabolisable (kJ, including dietary fibre)] DECIMAL(18,2),
[Energy, total metabolisable, available carbohydrate, FSANZ (kcal)] DECIMAL(18,2),
[Energy, total metabolisable, available carbohydrate, FSANZ (kJ)] DECIMAL(18,2),
[Energy, total metabolisable, carbohydrate by difference, FSANZ (kcal)] DECIMAL(18,2),
[Energy, total metabolisable, carbohydrate by difference, FSANZ (kJ)] DECIMAL(18,2),
[Fat, total] DECIMAL(18,2),
[Fatty acid 18:3 omega-3] DECIMAL(18,2),
[Fatty acid 20:5 omega-3] DECIMAL(18,2),
[Fatty acid 22:5 omega-3] DECIMAL(18,2),
[Fatty acid 22:6 omega-3] DECIMAL(18,2),
[Fatty acid cis, trans 18:2 omega-9, 11] DECIMAL(18,2),
[Fatty acid cis,cis 18:2 omega-6] DECIMAL(18,2),
[Fatty acids, total long chain polyunsaturated omega-3] DECIMAL(18,2),
[Fatty acids, total monounsaturated] DECIMAL(18,2),
[Fatty acids, total polyunsaturated] DECIMAL(18,2),
[Fatty acids, total polyunsaturated omega-3] DECIMAL(18,2),
[Fatty acids, total polyunsaturated omega-6] DECIMAL(18,2),
[Fatty acids, total saturated] DECIMAL(18,2),
[Fatty acids, total trans] DECIMAL(18,2),
[Fibre, total dietary] DECIMAL(18,2),
[Fibre, water-insoluble] DECIMAL(18,2),
[Fibre, water-soluble] DECIMAL(18,2),
[Folate food, naturally occurring food folates] DECIMAL(18,2),
[Folate, total] DECIMAL(18,2),
[Folic acid, synthetic folic acid] DECIMAL(18,2),
[Fructose] DECIMAL(18,2),
[Gamma-tocopherol] DECIMAL(18,2),
Glucose DECIMAL(18,2),
Iodide DECIMAL(18,2),
Iron DECIMAL(18,2),
Lactose DECIMAL(18,2),
Magnesium DECIMAL(18,2),
Maltose DECIMAL(18,2),
Manganese DECIMAL(18,2),
[Niacin equivalents from tryptophan] DECIMAL(18,2),
[Niacin equivalents, total] DECIMAL(18,2),
[Niacin, preformed] DECIMAL(18,2),
[Nitrogen, total] DECIMAL(18,2),
Phosphorus DECIMAL(18,2),
Potassium DECIMAL(18,2),
[Protein, total; calculated from total nitrogen] DECIMAL(18,2),
Retinol DECIMAL(18,2),
Riboflavin DECIMAL(18,2),
Selenium DECIMAL(18,2),
Sodium DECIMAL(18,2),
[Starch, total] DECIMAL(18,2),
Sucrose DECIMAL(18,2),
[Sugar, added] DECIMAL(18,2),
[Sugar, free] DECIMAL(18,2),
[Sugars, total] DECIMAL(18,2),
Thiamin DECIMAL(18,2),
[Total carbohydrate by difference] DECIMAL(18,2),
[Total carbohydrates by summation] DECIMAL(18,2),
Tryptophan DECIMAL(18,2),
[Vitamin A, retinol activity equivalents] DECIMAL(18,2),
[Vitamin A, retinol equivalents] DECIMAL(18,2),
[Vitamin B12] DECIMAL(18,2),
[Vitamin B6] DECIMAL(18,2),
[Vitamin C] DECIMAL(18,2),
[Vitamin D; calculated by summation] DECIMAL(18,2),
[Vitamin E, alpha-tocopherol equivalents] DECIMAL(18,2),
Water DECIMAL(18,2),
Zinc DECIMAL(18,2)
CONSTRAINT FoodID_fk3 FOREIGN KEY (FoodID) REFERENCES [NAME.FT](FoodID),
CONSTRAINT [DATA.AP_pk] PRIMARY KEY (FoodID)
)

CREATE TABLE UserDiary (
UserID INT,
FoodID VARCHAR(50),
[DATE] DATE DEFAULT CAST(GETDATE() AS DATE)
CONSTRAINT UserID1_fk FOREIGN KEY (UserID) REFERENCES Users(UserID),
CONSTRAINT FoodID_fk4 FOREIGN KEY (FoodID) REFERENCES [DATA.AP](FoodID),
CONSTRAINT UserDiary_pk PRIMARY KEY (UserID)
)