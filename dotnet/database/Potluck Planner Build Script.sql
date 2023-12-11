USE master
GO

--drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

CREATE TABLE recovery_questions (
	question_id INT IDENTITY (701,1) PRIMARY KEY,
	question_text NVARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE users (
    user_id INT IDENTITY(1,1) PRIMARY KEY,
	username NVARCHAR(50) NOT NULL UNIQUE,
    first_name NVARCHAR(100) NULL,
    last_name NVARCHAR(100) NULL,
    email NVARCHAR(100) NOT NULL,
    password_hash NVARCHAR(100) NOT NULL,
    salt NVARCHAR(50) NOT NULL,
    user_role NVARCHAR(20) NOT NULL DEFAULT 'user',
	user_summary NVARCHAR(MAX) NULL,
	diet_rest BIT NOT NULL DEFAULT 0
);

CREATE TABLE potlucks (
    potluck_id INT NOT NULL IDENTITY(1001,1) PRIMARY KEY,
    host_id INT NOT NULL FOREIGN KEY references users(user_id),
    potluck_name NVARCHAR(100) NOT NULL,
	summary NVARCHAR(MAX) NULL,
    location NVARCHAR(100) NOT NULL,
    time DATETIME NOT NULL,
    theme NVARCHAR(100) NULL,
    is_recurring BIT DEFAULT 0,
    repeat_interval INT NOT NULL DEFAULT 0,
	status NVARCHAR(20) NOT NULL DEFAULT 'active'
);

CREATE TABLE courses (
	course_id INT IDENTITY(1,1) PRIMARY KEY,
	course_name NVARCHAR(25) NOT NULL
);

CREATE TABLE dishes (
    dish_id INT NOT NULL IDENTITY(1501,1) PRIMARY KEY,
    dish_name NVARCHAR(50) NOT NULL,
    recipe NVARCHAR(MAX) NOT NULL,
	rating numeric(2,1) NULL,
	course_id int NOT NULL FOREIGN KEY references courses(course_id)
);

CREATE TABLE ingredients (
    ingredient_id INT NOT NULL IDENTITY(201,1) PRIMARY KEY,
    ingredient_name NVARCHAR(50) NOT NULL
);

CREATE TABLE diets (
	diet_id INT NOT NULL IDENTITY(601,1),
	diet_name NVARCHAR(30) NOT NULL PRIMARY KEY
);

CREATE TABLE titles (
    title_id INT NOT NULL IDENTITY(401,1) PRIMARY KEY,
    title_name NVARCHAR(20) NOT NULL
);

CREATE TABLE allergens (
	allergen_id INT NOT NULL IDENTITY(301,1) PRIMARY KEY,
	allergen_name NVARCHAR(30) NOT NULL,
);

CREATE TABLE user_title (
    user_id INT FOREIGN KEY references users(user_id),
    title_id INT FOREIGN KEY references titles(title_id),
    PRIMARY KEY (user_id, title_id)
);

CREATE TABLE user_dish (
    user_id INT FOREIGN KEY references users(user_id),
    dish_id INT FOREIGN KEY references dishes(dish_id),
    PRIMARY KEY (user_id, dish_id),
);

CREATE TABLE potluck_dish (
    potluck_id INT FOREIGN KEY references potlucks(potluck_id),
    dish_id INT FOREIGN KEY references dishes(dish_id),
    PRIMARY KEY (potluck_id, dish_id)
);

CREATE TABLE potluck_user (
    potluck_id INT FOREIGN KEY references potlucks(potluck_id),
    user_id INT FOREIGN KEY references users(user_id),
	status NVARCHAR(20) NOT NULL DEFAULT 'Invited', --Attending, Not Attenging, etc.
    PRIMARY KEY (potluck_id, user_id)    
);

CREATE TABLE invitations (
    potluck_id INT FOREIGN KEY references potlucks(potluck_id),
    email NVARCHAR(200) NOT NULL,
	status NVARCHAR(30) NOT NULL DEFAULT 'Invited', --Attending, Not Attenging, etc.
    PRIMARY KEY (potluck_id, email)    
);

CREATE TABLE ingredient_dish (
    ingredient_id INT FOREIGN KEY references ingredients(ingredient_id),
    dish_id INT FOREIGN KEY references dishes(dish_id),
	quantity NVARCHAR(50) NOT NULL,
    PRIMARY KEY (ingredient_id, dish_id)
);

CREATE TABLE user_allergies (
    user_id INT FOREIGN KEY references users(user_id),
    ingredient_id INT FOREIGN KEY references ingredients(ingredient_id),
    PRIMARY KEY (ingredient_id, user_id)
);

CREATE TABLE potluck_diet (
	potluck_id INT FOREIGN KEY references potlucks(potluck_id),
	diet_name NVARCHAR(30) FOREIGN KEY references diets(diet_name),
	PRIMARY KEY (potluck_id, diet_name)
);

CREATE TABLE dish_diet (
	dish_id INT FOREIGN KEY references dishes(dish_id),
	diet_name NVARCHAR(30) FOREIGN KEY references diets(diet_name),
	PRIMARY KEY (dish_id, diet_name)
);

CREATE TABLE diet_ingredient (
	diet_name NVARCHAR(30) FOREIGN KEY references diets(diet_name),
    ingredient_id INT FOREIGN KEY references ingredients(ingredient_id),
	PRIMARY KEY (diet_name, ingredient_id)
);

CREATE TABLE user_diet (
    user_id INT FOREIGN KEY references users(user_id),
	diet_name NVARCHAR(30) FOREIGN KEY references diets(diet_name),
	PRIMARY KEY (user_id, diet_name)
);

CREATE TABLE friends_list (
	self_id INT FOREIGN KEY references users(user_id),
	friend_id INT FOREIGN KEY references users(user_id),
	friend_start DATETIME NULL,
	PRIMARY KEY (self_id, friend_id)
);

CREATE TABLE dish_rating (
	dish_id INT FOREIGN KEY references dishes(dish_id),
	rater INT FOREIGN KEY references users(user_id),
	rating INT NOT NULL
	PRIMARY KEY (dish_id, rater)
);

CREATE TABLE potluck_course (
	potluck_id INT FOREIGN KEY references potlucks(potluck_id),
	course_id INT FOREIGN KEY references courses(course_id),
	how_many INT NOT NULL, 
	PRIMARY KEY (potluck_id, course_id)
);

CREATE TABLE user_recovery(
	user_id INT FOREIGN KEY references users(user_id),
	question_id INT FOREIGN KEY references recovery_questions(question_id),
	answer NVARCHAR(100) NOT NULL
);

--populate default data

INSERT INTO titles (title_name)
VALUES ('host'),('guest');
INSERT INTO diets (diet_name)
VALUES ('vegan'), ('vegetarian'), ('halal'), ('celiac'), ('lactose'), ('kosher');

SET IDENTITY_INSERT [dbo].[courses] ON 

INSERT [dbo].[courses] ([course_id], [course_name]) VALUES (1, N'apps')
INSERT [dbo].[courses] ([course_id], [course_name]) VALUES (2, N'main')
INSERT [dbo].[courses] ([course_id], [course_name]) VALUES (3, N'side')
INSERT [dbo].[courses] ([course_id], [course_name]) VALUES (4, N'dessert')
SET IDENTITY_INSERT [dbo].[courses] OFF
GO

SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([user_id], [username], [first_name], [last_name], [email], [password_hash], [salt], [user_role], [user_summary], [diet_rest]) VALUES (1, N'user', NULL, NULL, N'user@gmail.com', N'Jg45HuwT7PZkfuKTz6IB90CtWY4=', N'LHxP4Xh7bN0=', N'user', NULL, 0)
INSERT [dbo].[users] ([user_id], [username], [first_name], [last_name], [email], [password_hash], [salt], [user_role], [user_summary], [diet_rest]) VALUES (2, N'admin', NULL, NULL, N'admin@gmail.com', N'YhyGVQ+Ch69n4JMBncM4lNF/i9s=', N'Ar/aB2thQTI=', N'admin', NULL, 0)
INSERT [dbo].[users] ([user_id], [username], [first_name], [last_name], [email], [password_hash], [salt], [user_role], [user_summary], [diet_rest]) VALUES (3, N'ted', NULL, NULL, N'ted@gmail.com', N'tH8BvtmnyMcS5Q9Oxlyiup5IqUQ=', N'yA4lHw0idGs=', N'admin', NULL, 0)
INSERT [dbo].[users] ([user_id], [username], [first_name], [last_name], [email], [password_hash], [salt], [user_role], [user_summary], [diet_rest]) VALUES (4, N'kelsey', NULL, NULL, N'kelsey@gmail.com', N'IscjyGodaIc9xNhASbt+fSnxgtI=', N'XdiczNYbBno=', N'admin', NULL, 0)
INSERT [dbo].[users] ([user_id], [username], [first_name], [last_name], [email], [password_hash], [salt], [user_role], [user_summary], [diet_rest]) VALUES (5, N'robert', NULL, NULL, N'robert@gmail.com', N'pfA+TsxwG1V0u7AE7Tqagl1fT1A=', N'4L75U8vaKT0=', N'admin', NULL, 0)
INSERT [dbo].[users] ([user_id], [username], [first_name], [last_name], [email], [password_hash], [salt], [user_role], [user_summary], [diet_rest]) VALUES (6, N'kyle', NULL, NULL, N'kyle@gmail.com', N'Fjhd4G3BwUgJ4SYdtL1oWk8eFPo=', N'xWO2yPBd7Sg=', N'admin', NULL, 0)
INSERT [dbo].[users] ([user_id], [username], [first_name], [last_name], [email], [password_hash], [salt], [user_role], [user_summary], [diet_rest]) VALUES (7, N'matt', NULL, NULL, N'matt@gmail.com', N'XrITqHZWKG4kOj1U95ema/cbb20=', N'8Vsd/EnDrOM=', N'admin', NULL, 0)
SET IDENTITY_INSERT [dbo].[users] OFF
GO


SET IDENTITY_INSERT [dbo].[potlucks] ON 

INSERT [dbo].[potlucks] ([potluck_id], [host_id], [potluck_name], [summary], [location], [time], [theme], [is_recurring], [repeat_interval], [status]) VALUES (1002, 7, N'Matt''s Potluck', N'dig it', N'My House', CAST(N'2023-12-10T14:00:00.000' AS DateTime), N'Winter', 0, 0, N'inactive')
INSERT [dbo].[potlucks] ([potluck_id], [host_id], [potluck_name], [summary], [location], [time], [theme], [is_recurring], [repeat_interval], [status]) VALUES (1003, 7, N'Mott''s Patluck', N'come eat', N'Under a Bridge', CAST(N'2023-12-12T16:00:00.000' AS DateTime), N'Winter', 0, 0, N'inactive')
INSERT [dbo].[potlucks] ([potluck_id], [host_id], [potluck_name], [summary], [location], [time], [theme], [is_recurring], [repeat_interval], [status]) VALUES (1004, 7, N'Patt''s Motluck', N'I am fine with you people', N'Under the earth', CAST(N'2023-12-15T12:00:00.000' AS DateTime), N'Winter', 0, 0, N'active')
INSERT [dbo].[potlucks] ([potluck_id], [host_id], [potluck_name], [summary], [location], [time], [theme], [is_recurring], [repeat_interval], [status]) VALUES (1005, 6, N'Kyle''s Potluck', N'food moment', N'His Place', CAST(N'2023-12-08T11:00:00.000' AS DateTime), N'Winter', 0, 0, N'active')
INSERT [dbo].[potlucks] ([potluck_id], [host_id], [potluck_name], [summary], [location], [time], [theme], [is_recurring], [repeat_interval], [status]) VALUES (1006, 6, N'Kyle loves potlucks', N'It''s true.', N'His Place', CAST(N'2023-12-09T17:00:00.000' AS DateTime), N'Winter', 0, 0, N'active')
INSERT [dbo].[potlucks] ([potluck_id], [host_id], [potluck_name], [summary], [location], [time], [theme], [is_recurring], [repeat_interval], [status]) VALUES (1007, 6, N'Enough is enough, Kyle.', N'The final Hootinany', N'His Garage', CAST(N'2023-12-17T18:00:00.000' AS DateTime), N'Winter', 0, 0, N'active')
INSERT [dbo].[potlucks] ([potluck_id], [host_id], [potluck_name], [summary], [location], [time], [theme], [is_recurring], [repeat_interval], [status]) VALUES (1008, 4, N'Kelsey''s Potluck', N'We eatin out here', N'Her House', CAST(N'2023-12-08T14:00:00.000' AS DateTime), N'Winter', 0, 0, N'active')
INSERT [dbo].[potlucks] ([potluck_id], [host_id], [potluck_name], [summary], [location], [time], [theme], [is_recurring], [repeat_interval], [status]) VALUES (1009, 4, N'Kelsey''s other potluck', N'Bring cigs', N'Her porch', CAST(N'2023-12-16T16:00:00.000' AS DateTime), N'Winter', 0, 0, N'active')
INSERT [dbo].[potlucks] ([potluck_id], [host_id], [potluck_name], [summary], [location], [time], [theme], [is_recurring], [repeat_interval], [status]) VALUES (1010, 4, N'Cigarette Caseroles Only', N'I''m not joking', N'Dahn the block', CAST(N'2023-12-25T06:00:00.000' AS DateTime), N'Winter', 0, 0, N'active')
INSERT [dbo].[potlucks] ([potluck_id], [host_id], [potluck_name], [summary], [location], [time], [theme], [is_recurring], [repeat_interval], [status]) VALUES (1011, 7, N'Newest Potluck', N'asdf', N'Wherever I want', CAST(N'2023-12-08T16:17:00.000' AS DateTime), N'Spring', 0, 0, N'active')
INSERT [dbo].[potlucks] ([potluck_id], [host_id], [potluck_name], [summary], [location], [time], [theme], [is_recurring], [repeat_interval], [status]) VALUES (1012, 7, N'aa', N'a', N'asdf', CAST(N'2023-12-12T16:18:00.000' AS DateTime), N'Fall', 0, 0, N'active')
INSERT [dbo].[potlucks] ([potluck_id], [host_id], [potluck_name], [summary], [location], [time], [theme], [is_recurring], [repeat_interval], [status]) VALUES (1013, 7, N'asdf', N'asdf', N'asdf', CAST(N'2023-12-20T19:09:00.000' AS DateTime), N'Summer', 0, 0, N'active')
INSERT [dbo].[potlucks] ([potluck_id], [host_id], [potluck_name], [summary], [location], [time], [theme], [is_recurring], [repeat_interval], [status]) VALUES (1014, 6, N'Testing!', N'summary', N'wherever I''m not picky', CAST(N'2023-12-13T08:11:00.000' AS DateTime), N'Summer', 0, 0, N'active')
SET IDENTITY_INSERT [dbo].[potlucks] OFF
GO

SET IDENTITY_INSERT [dbo].[dishes] ON 

INSERT [dbo].[dishes] ([dish_id], [dish_name], [recipe], [rating], [course_id]) VALUES (1504, N'Matt''s Lunch', N'Rotini, Red Sauce, Chicken, Broccoli', CAST(3.0 AS Numeric(2, 1)), 1)
INSERT [dbo].[dishes] ([dish_id], [dish_name], [recipe], [rating], [course_id]) VALUES (1505, N'Matt''s Dinner', N'3 eggs, 4 egg whites, 2 english muffins, salsa, apple', CAST(3.5 AS Numeric(2, 1)), 1)
INSERT [dbo].[dishes] ([dish_id], [dish_name], [recipe], [rating], [course_id]) VALUES (1506, N'Matt''s Breakfast', N'80 grams of quick oats, 1 scoop of whey protein, 15g walnuts, 1 banana', CAST(4.0 AS Numeric(2, 1)), 1)
INSERT [dbo].[dishes] ([dish_id], [dish_name], [recipe], [rating], [course_id]) VALUES (1507, N'Matt''s Midnight Snack', N'Pint of Ben & Jerry''s', CAST(5.0 AS Numeric(2, 1)), 2)
INSERT [dbo].[dishes] ([dish_id], [dish_name], [recipe], [rating], [course_id]) VALUES (1508, N'Ted''s Chili', N'Idk ask his wife', CAST(5.0 AS Numeric(2, 1)), 1)
INSERT [dbo].[dishes] ([dish_id], [dish_name], [recipe], [rating], [course_id]) VALUES (1509, N'Ted''s Soup', N'Idk ask him', CAST(5.0 AS Numeric(2, 1)), 1)
INSERT [dbo].[dishes] ([dish_id], [dish_name], [recipe], [rating], [course_id]) VALUES (1510, N'Aseel''s Tiramisu Cheesecake', N'...ask him', CAST(5.0 AS Numeric(2, 1)), 2)
INSERT [dbo].[dishes] ([dish_id], [dish_name], [recipe], [rating], [course_id]) VALUES (1511, N'This is an Appetizer', N'who cares', CAST(3.0 AS Numeric(2, 1)), 3)
SET IDENTITY_INSERT [dbo].[dishes] OFF
GO


INSERT [dbo].[potluck_course] ([potluck_id], [course_id], [how_many]) VALUES (1002, 1, 3)
INSERT [dbo].[potluck_course] ([potluck_id], [course_id], [how_many]) VALUES (1002, 2, 4)
INSERT [dbo].[potluck_course] ([potluck_id], [course_id], [how_many]) VALUES (1002, 3, 5)
INSERT [dbo].[potluck_course] ([potluck_id], [course_id], [how_many]) VALUES (1002, 4, 2)
INSERT [dbo].[potluck_course] ([potluck_id], [course_id], [how_many]) VALUES (1003, 1, 4)
INSERT [dbo].[potluck_course] ([potluck_id], [course_id], [how_many]) VALUES (1003, 2, 4)
INSERT [dbo].[potluck_course] ([potluck_id], [course_id], [how_many]) VALUES (1003, 3, 4)
INSERT [dbo].[potluck_course] ([potluck_id], [course_id], [how_many]) VALUES (1003, 4, 4)
INSERT [dbo].[potluck_course] ([potluck_id], [course_id], [how_many]) VALUES (1004, 1, 4)
INSERT [dbo].[potluck_course] ([potluck_id], [course_id], [how_many]) VALUES (1004, 2, 4)
INSERT [dbo].[potluck_course] ([potluck_id], [course_id], [how_many]) VALUES (1004, 3, 4)
INSERT [dbo].[potluck_course] ([potluck_id], [course_id], [how_many]) VALUES (1004, 4, 4)
INSERT [dbo].[potluck_course] ([potluck_id], [course_id], [how_many]) VALUES (1011, 1, 4)
INSERT [dbo].[potluck_course] ([potluck_id], [course_id], [how_many]) VALUES (1011, 2, 4)
INSERT [dbo].[potluck_course] ([potluck_id], [course_id], [how_many]) VALUES (1011, 3, 4)
INSERT [dbo].[potluck_course] ([potluck_id], [course_id], [how_many]) VALUES (1011, 4, 4)
INSERT [dbo].[potluck_course] ([potluck_id], [course_id], [how_many]) VALUES (1012, 1, 2)
INSERT [dbo].[potluck_course] ([potluck_id], [course_id], [how_many]) VALUES (1012, 2, 5)
INSERT [dbo].[potluck_course] ([potluck_id], [course_id], [how_many]) VALUES (1012, 3, 4)
INSERT [dbo].[potluck_course] ([potluck_id], [course_id], [how_many]) VALUES (1012, 4, 1)
INSERT [dbo].[potluck_course] ([potluck_id], [course_id], [how_many]) VALUES (1013, 1, 2)
INSERT [dbo].[potluck_course] ([potluck_id], [course_id], [how_many]) VALUES (1013, 2, 2)
INSERT [dbo].[potluck_course] ([potluck_id], [course_id], [how_many]) VALUES (1013, 3, 2)
INSERT [dbo].[potluck_course] ([potluck_id], [course_id], [how_many]) VALUES (1013, 4, 2)
INSERT [dbo].[potluck_course] ([potluck_id], [course_id], [how_many]) VALUES (1014, 1, 1)
INSERT [dbo].[potluck_course] ([potluck_id], [course_id], [how_many]) VALUES (1014, 2, 1)
INSERT [dbo].[potluck_course] ([potluck_id], [course_id], [how_many]) VALUES (1014, 3, 1)
INSERT [dbo].[potluck_course] ([potluck_id], [course_id], [how_many]) VALUES (1014, 4, 1)
GO
INSERT [dbo].[potluck_dish] ([potluck_id], [dish_id]) VALUES (1002, 1504)
INSERT [dbo].[potluck_dish] ([potluck_id], [dish_id]) VALUES (1002, 1505)
INSERT [dbo].[potluck_dish] ([potluck_id], [dish_id]) VALUES (1002, 1506)
INSERT [dbo].[potluck_dish] ([potluck_id], [dish_id]) VALUES (1002, 1507)
INSERT [dbo].[potluck_dish] ([potluck_id], [dish_id]) VALUES (1002, 1508)
INSERT [dbo].[potluck_dish] ([potluck_id], [dish_id]) VALUES (1002, 1509)
INSERT [dbo].[potluck_dish] ([potluck_id], [dish_id]) VALUES (1002, 1510)
INSERT [dbo].[potluck_dish] ([potluck_id], [dish_id]) VALUES (1002, 1511)
INSERT [dbo].[potluck_dish] ([potluck_id], [dish_id]) VALUES (1003, 1505)
INSERT [dbo].[potluck_dish] ([potluck_id], [dish_id]) VALUES (1003, 1507)
INSERT [dbo].[potluck_dish] ([potluck_id], [dish_id]) VALUES (1003, 1509)
INSERT [dbo].[potluck_dish] ([potluck_id], [dish_id]) VALUES (1003, 1511)
INSERT [dbo].[potluck_dish] ([potluck_id], [dish_id]) VALUES (1004, 1504)
INSERT [dbo].[potluck_dish] ([potluck_id], [dish_id]) VALUES (1004, 1506)
INSERT [dbo].[potluck_dish] ([potluck_id], [dish_id]) VALUES (1005, 1508)
INSERT [dbo].[potluck_dish] ([potluck_id], [dish_id]) VALUES (1005, 1510)
INSERT [dbo].[potluck_dish] ([potluck_id], [dish_id]) VALUES (1006, 1504)
INSERT [dbo].[potluck_dish] ([potluck_id], [dish_id]) VALUES (1006, 1505)
INSERT [dbo].[potluck_dish] ([potluck_id], [dish_id]) VALUES (1006, 1506)
INSERT [dbo].[potluck_dish] ([potluck_id], [dish_id]) VALUES (1006, 1507)
INSERT [dbo].[potluck_dish] ([potluck_id], [dish_id]) VALUES (1007, 1510)
INSERT [dbo].[potluck_dish] ([potluck_id], [dish_id]) VALUES (1007, 1511)
INSERT [dbo].[potluck_dish] ([potluck_id], [dish_id]) VALUES (1008, 1508)
INSERT [dbo].[potluck_dish] ([potluck_id], [dish_id]) VALUES (1008, 1509)
INSERT [dbo].[potluck_dish] ([potluck_id], [dish_id]) VALUES (1009, 1509)
INSERT [dbo].[potluck_dish] ([potluck_id], [dish_id]) VALUES (1009, 1510)
INSERT [dbo].[potluck_dish] ([potluck_id], [dish_id]) VALUES (1009, 1511)
INSERT [dbo].[potluck_dish] ([potluck_id], [dish_id]) VALUES (1010, 1505)
GO
INSERT [dbo].[potluck_user] ([potluck_id], [user_id], [status]) VALUES (1002, 7, N'inactive')
INSERT [dbo].[potluck_user] ([potluck_id], [user_id], [status]) VALUES (1003, 7, N'inactive')
INSERT [dbo].[potluck_user] ([potluck_id], [user_id], [status]) VALUES (1004, 7, N'Active')
INSERT [dbo].[potluck_user] ([potluck_id], [user_id], [status]) VALUES (1005, 6, N'Active')
INSERT [dbo].[potluck_user] ([potluck_id], [user_id], [status]) VALUES (1006, 6, N'Active')
INSERT [dbo].[potluck_user] ([potluck_id], [user_id], [status]) VALUES (1007, 6, N'Active')
INSERT [dbo].[potluck_user] ([potluck_id], [user_id], [status]) VALUES (1008, 4, N'Active')
INSERT [dbo].[potluck_user] ([potluck_id], [user_id], [status]) VALUES (1009, 4, N'Active')
INSERT [dbo].[potluck_user] ([potluck_id], [user_id], [status]) VALUES (1010, 4, N'Active')
INSERT [dbo].[potluck_user] ([potluck_id], [user_id], [status]) VALUES (1011, 7, N'Active')
INSERT [dbo].[potluck_user] ([potluck_id], [user_id], [status]) VALUES (1012, 7, N'Active')
INSERT [dbo].[potluck_user] ([potluck_id], [user_id], [status]) VALUES (1013, 7, N'Active')
INSERT [dbo].[potluck_user] ([potluck_id], [user_id], [status]) VALUES (1014, 6, N'Active')
GO


INSERT [dbo].[user_dish] ([user_id], [dish_id]) VALUES (3, 1508)
INSERT [dbo].[user_dish] ([user_id], [dish_id]) VALUES (3, 1509)
INSERT [dbo].[user_dish] ([user_id], [dish_id]) VALUES (4, 1510)
INSERT [dbo].[user_dish] ([user_id], [dish_id]) VALUES (4, 1511)
INSERT [dbo].[user_dish] ([user_id], [dish_id]) VALUES (5, 1511)
INSERT [dbo].[user_dish] ([user_id], [dish_id]) VALUES (6, 1504)
INSERT [dbo].[user_dish] ([user_id], [dish_id]) VALUES (6, 1505)
INSERT [dbo].[user_dish] ([user_id], [dish_id]) VALUES (6, 1506)
INSERT [dbo].[user_dish] ([user_id], [dish_id]) VALUES (6, 1507)
INSERT [dbo].[user_dish] ([user_id], [dish_id]) VALUES (7, 1504)
INSERT [dbo].[user_dish] ([user_id], [dish_id]) VALUES (7, 1505)
INSERT [dbo].[user_dish] ([user_id], [dish_id]) VALUES (7, 1506)
INSERT [dbo].[user_dish] ([user_id], [dish_id]) VALUES (7, 1507)
GO

INSERT INTO invitations (potluck_id, email) 
VALUES 
(1002, 'admin@gmail.com'), 
(1002, 'kelsey@gmail.com'),
(1002, 'kyle@gmail.com'),
(1002, 'matt@gmail.com'),
(1002, 'robert@gmail.com'),
(1002, 'ted@gmail.com'),
(1002, 'user@gmail.com');

INSERT INTO allergens (allergen_name)
VALUES ('fish'), ('shellfish'), ('milk'), ('egg'), ('soy'), ('sesame'), ('treenuts'), ('peanuts'), ('wheat');


