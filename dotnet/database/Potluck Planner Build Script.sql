USE master
GO

--drop database if it exists
IF DB_ID('potluck_planner') IS NOT NULL
BEGIN
	ALTER DATABASE potluck_planner SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE potluck_planner;
END

CREATE DATABASE potluck_planner
GO

USE potluck_planner
GO




CREATE TABLE users (
    user_id INT IDENTITY(1,1) PRIMARY KEY,
	username NVARCHAR(50) NOT NULL UNIQUE,
    first_name NVARCHAR(100) NULL,
    last_name NVARCHAR(100) NULL,
    email NVARCHAR(100) NULL,
    password_hash NVARCHAR(100) NOT NULL,
    salt NVARCHAR(50) NOT NULL,
    user_role NVARCHAR(20) NOT NULL DEFAULT 'user',
	user_summary NVARCHAR(MAX) NULL
);

CREATE TABLE potlucks (
    potluck_ID INT NOT NULL IDENTITY(1001,1) PRIMARY KEY,
    host_ID INT NOT NULL FOREIGN KEY REFERENCES users(user_id),
    potluck_name NVARCHAR(100) NOT NULL,
	summary NVARCHAR(MAX) NULL,
    location NVARCHAR(100) NOT NULL,
    time DATETIME NOT NULL,
    theme NVARCHAR(100) NULL,
    is_recurring BIT DEFAULT 0,
    repeat_interval INT NULL
);

CREATE TABLE dishes (
    dish_ID INT NOT NULL IDENTITY(1501,1) PRIMARY KEY,
    dish_name NVARCHAR(50) NOT NULL,
    recipe NVARCHAR(MAX) NOT NULL
);

CREATE TABLE titles (
    title_id INT NOT NULL IDENTITY(401,1) PRIMARY KEY,
    title_name NVARCHAR(20) NOT NULL
);

CREATE TABLE ingredients (
    ingredient_id INT NOT NULL IDENTITY(201,1) PRIMARY KEY,
    ingredient_name NVARCHAR(50) NOT NULL
);

CREATE TABLE user_title (
    user_id INT NOT NULL,
    title_id INT NOT NULL,
    PRIMARY KEY (user_id, title_id),
    FOREIGN KEY (user_id) REFERENCES users(user_id),
    FOREIGN KEY (title_id) REFERENCES titles(title_id)
);

CREATE TABLE user_dish (
    user_id INT NOT NULL,
    dish_id INT NOT NULL,
    PRIMARY KEY (user_id, dish_id),
    FOREIGN KEY (user_id) REFERENCES users(user_id),
    FOREIGN KEY (dish_id) REFERENCES dishes(dish_id)
);

CREATE TABLE potluck_dish (
    potluck_id INT NOT NULL,
    dish_id INT NOT NULL,
    PRIMARY KEY (potluck_id, dish_id),
    FOREIGN KEY (potluck_id) REFERENCES potlucks(potluck_id),
    FOREIGN KEY (dish_id) REFERENCES dishes(dish_id)
);

CREATE TABLE potluck_user (
    potluck_id INT NOT NULL,
    user_id INT NOT NULL,
	status NVARCHAR(20) NOT NULL DEFAULT 'Invited', --Attending, Not Attenging
    PRIMARY KEY (potluck_id, user_id),
    FOREIGN KEY (potluck_id) REFERENCES potlucks(potluck_id),
    FOREIGN KEY (user_id) REFERENCES users(user_id),
);

CREATE TABLE ingredient_dish (
    ingredient_id INT NOT NULL,
    dish_id INT NOT NULL,
	quantity NVARCHAR(50) NOT NULL,
    PRIMARY KEY (ingredient_id, dish_id),
    FOREIGN KEY (ingredient_id) REFERENCES ingredients(ingredient_id),
    FOREIGN KEY (dish_id) REFERENCES dishes(dish_id)
);

CREATE TABLE user_ingredient (
    ingredient_id INT NOT NULL,
    user_id INT NOT NULL,
    PRIMARY KEY (ingredient_id, user_id),
    FOREIGN KEY (ingredient_id) REFERENCES ingredients(ingredient_id),
    FOREIGN KEY (user_id) REFERENCES users(user_id),
);


--populate default data
INSERT INTO users (username, password_hash, salt, user_role) 
VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) 
VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');
INSERT INTO users (username, password_hash, salt, user_role) 
VALUES ('ted','tH8BvtmnyMcS5Q9Oxlyiup5IqUQ=', 'yA4lHw0idGs=','admin');
INSERT INTO users (username, password_hash, salt, user_role) 
VALUES ('kelsey','IscjyGodaIc9xNhASbt+fSnxgtI=', 'XdiczNYbBno=','admin');
INSERT INTO users (username, password_hash, salt, user_role) 
VALUES ('robert','pfA+TsxwG1V0u7AE7Tqagl1fT1A=', '4L75U8vaKT0=','admin');
INSERT INTO users (username, password_hash, salt, user_role) 
VALUES ('kyle','Fjhd4G3BwUgJ4SYdtL1oWk8eFPo=', 'xWO2yPBd7Sg=','user');
INSERT INTO users (username, password_hash, salt, user_role) 
VALUES ('matt','XrITqHZWKG4kOj1U95ema/cbb20=', '8Vsd/EnDrOM=','user');
INSERT INTO title (title_name)
VALUES ('host'),('creator'),('guest');
GO




