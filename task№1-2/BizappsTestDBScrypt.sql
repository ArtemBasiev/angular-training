
--    Перед выполнением скрипта создайте базу данных с именем BizappsTest
  USE BizappsTest
  GO 
--
-- Создать таблицу [dbo].[BlogUser]
--

CREATE TABLE dbo.BlogUser (
  Id int IDENTITY,
  UserName varchar(30) NOT NULL,
  UserPassword varchar(20) NOT NULL,
  CONSTRAINT PK_User_ID PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO


--
-- Создать таблицу [dbo].[Blog]
--
CREATE TABLE dbo.Blog (
  Id int IDENTITY,
  BlogName varchar(40) NOT NULL,
  CreationDate date NOT NULL,
  UserId int NOT NULL,
  CONSTRAINT PK_Blog_Id PRIMARY KEY CLUSTERED (Id),
  CONSTRAINT KEY_Blog_UserId UNIQUE (UserId)
)
ON [PRIMARY]
GO

--
-- Создать внешний ключ [FK_Blog_UserId] для объекта типа таблица [dbo].[Blog]
--
ALTER TABLE dbo.Blog
  ADD CONSTRAINT FK_Blog_UserId FOREIGN KEY (UserId) REFERENCES dbo.BlogUser (Id)
GO




--
-- Создать таблицу [dbo].[Post]
--

CREATE TABLE dbo.Post (
  Id int IDENTITY,
  Title varchar(50) NOT NULL,
  Body text NULL,
  CreationDate date NOT NULL,
  BlogId int NOT NULL,
  CONSTRAINT PK_Post_Id PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
TEXTIMAGE_ON [PRIMARY]
GO

--
-- Создать внешний ключ [FK_Post_BlogId] для объекта типа таблица [dbo].[Post]
--
ALTER TABLE dbo.Post
  ADD CONSTRAINT FK_Post_BlogId FOREIGN KEY (BlogId) REFERENCES dbo.Blog (Id)
GO


--
-- Создать таблицу [dbo].[Category]
--

CREATE TABLE dbo.Category (
  Id int IDENTITY,
  CategoryName varchar(50) NOT NULL,
  CONSTRAINT PK_Category_Id PRIMARY KEY CLUSTERED (Id),
  CONSTRAINT KEY_Category_CategoryName UNIQUE (CategoryName)
)
ON [PRIMARY]
GO



--
-- Создать таблицу [dbo].[RelationPostCategory]
--

CREATE TABLE dbo.RelationPostCategory (
  PostId int NOT NULL,
  CategoryId int NOT NULL,
  CONSTRAINT PK_RelationPostCategory PRIMARY KEY CLUSTERED (PostId, CategoryId)
)
ON [PRIMARY]
GO

--
-- Создать внешний ключ [FK_RelationPostCategory_CategoryId] для объекта типа таблица [dbo].[RelationPostCategory]
--

ALTER TABLE dbo.RelationPostCategory
  ADD CONSTRAINT FK_RelationPostCategory_CategoryId FOREIGN KEY (CategoryId) REFERENCES dbo.Category (Id)
GO

--
-- Создать внешний ключ [FK_RelationPostCategory_PostId] для объекта типа таблица [dbo].[RelationPostCategory]
--

ALTER TABLE dbo.RelationPostCategory
  ADD CONSTRAINT FK_RelationPostCategory_PostId FOREIGN KEY (PostId) REFERENCES dbo.Post (Id)
GO


--
-- Создать таблицу [dbo].[RelationBlogCategory]
--

CREATE TABLE dbo.RelationBlogCategory (
  BlogId int NOT NULL,
  CategoryId int NOT NULL,
  CONSTRAINT PK_RelationBlogCategory PRIMARY KEY CLUSTERED (BlogId, CategoryId)
)
ON [PRIMARY]
GO

--
-- Создать внешний ключ [FK_RelationBlogCategory_BlogId] для объекта типа таблица [dbo].[RelationBlogCategory]
--

ALTER TABLE dbo.RelationBlogCategory
  ADD CONSTRAINT FK_RelationBlogCategory_BlogId FOREIGN KEY (BlogId) REFERENCES dbo.Blog (Id)
GO

--
-- Создать внешний ключ [FK_RelationBlogCategory_CategoryId] для объекта типа таблица [dbo].[RelationBlogCategory]
--

ALTER TABLE dbo.RelationBlogCategory
  ADD CONSTRAINT FK_RelationBlogCategory_CategoryId FOREIGN KEY (CategoryId) REFERENCES dbo.Category (Id)
GO




--
-- Создать процедуру [dbo].[InsertCategory]
--

CREATE PROCEDURE dbo.InsertCategory
(
  @CategoryName VARCHAR(50),
  @ErrorMessage VARCHAR(50) OUTPUT
)
AS 
BEGIN
  DECLARE @ErrorNumber INT;

  IF ((RTRIM(LTRIM(@CategoryName)))='')
    BEGIN
    SET @CategoryName = NULL; 
    
    END ;
  BEGIN TRY
     INSERT INTO Category(
                 CategoryName)
                VALUES (
                 @CategoryName);
   
     SET @ErrorMessage = '0';

  END TRY
 BEGIN CATCH
  SET @ErrorNumber = ERROR_NUMBER();
  IF (@ErrorNumber =2627)
    BEGIN
      SET @ErrorMessage ='Введенная категория уже существует';
    END
  IF (@ErrorNumber =515)
    BEGIN
      SET @ErrorMessage ='Введите наименование категории';
    END

 END CATCH
  	
  END
  
GO



--
-- Создать функцию [dbo].[GetCategoryName]
--
CREATE FUNCTION dbo.GetCategoryName()
RETURNS 
  @table TABLE 
(
  CategoryName VARCHAR(50)
)
AS
  BEGIN

    INSERT INTO @table
	select CategoryName
	  from Category
	RETURN
  END

GO


--
-- Создать функцию [dbo].[GetErrorMessage]
--
CREATE FUNCTION dbo.GetErrorMessage(
  @ErrorMessage VARCHAR(50)

  )
RETURNS 
  @table TABLE 
(
  ErrorMessage VARCHAR(50)
)
AS
  BEGIN

    INSERT INTO @table (ErrorMessage)
	VALUES  (@ErrorMessage);
	RETURN
  END
GO


USE BizappsTest
 GO 
--
-- Заполним таблицу BlogUser
--
  SET IDENTITY_INSERT BlogUser ON

 INSERT INTO dbo.BlogUser(Id, UserName, UserPassword) VALUES(2, 'userA', '12334')
GO
INSERT INTO dbo.BlogUser(Id, UserName, UserPassword) VALUES(4, 'userB', '3456')
GO
INSERT INTO dbo.BlogUser(Id, UserName, UserPassword) VALUES(5, 'userC', 'dftt')
GO
INSERT INTO dbo.BlogUser(Id, UserName, UserPassword) VALUES(6, 'userD', '11111')
GO




--
-- Заполним таблицу Blog
--

 USE BizappsTest
 GO 

  SET IDENTITY_INSERT Blog ON

SET DATEFORMAT ymd
INSERT INTO dbo.Blog(Id, BlogName, CreationDate, UserId) VALUES(2, 'blogUserA', '2019-01-10', 2)
GO
INSERT INTO dbo.Blog(Id, BlogName, CreationDate, UserId) VALUES(4, 'blogUserB', '2019-01-11', 4)
GO
INSERT INTO dbo.Blog(Id, BlogName, CreationDate, UserId) VALUES(6, 'blogUserC', '2019-01-03', 5)
GO
INSERT INTO dbo.Blog(Id, BlogName, CreationDate, UserId) VALUES(7, 'bloguserD', '2019-01-05', 6)
GO





  --
-- Заполним таблицу Post
--
    USE BizappsTest
 GO 
  SET IDENTITY_INSERT Post ON
SET DATEFORMAT ymd
INSERT INTO dbo.Post(Id, Title, Body, CreationDate, BlogId) VALUES(1, 'postAblog', NULL, '2019-01-11', 2)
GO
INSERT INTO dbo.Post(Id, Title, Body, CreationDate, BlogId) VALUES(3, 'postAblog2', NULL, '2019-01-11', 2)
GO
INSERT INTO dbo.Post(Id, Title, Body, CreationDate, BlogId) VALUES(4, 'postAblog3', NULL, '2019-01-12', 2)
GO
INSERT INTO dbo.Post(Id, Title, Body, CreationDate, BlogId) VALUES(5, 'postBblog1', NULL, '2019-01-10', 4)
GO
INSERT INTO dbo.Post(Id, Title, Body, CreationDate, BlogId) VALUES(6, 'postBblog2', NULL, '2019-01-11', 4)
GO
INSERT INTO dbo.Post(Id, Title, Body, CreationDate, BlogId) VALUES(7, 'postBblog3', NULL, '2019-01-10', 4)
GO
INSERT INTO dbo.Post(Id, Title, Body, CreationDate, BlogId) VALUES(8, 'postCblog1', NULL, '2019-01-11', 6)
GO
INSERT INTO dbo.Post(Id, Title, Body, CreationDate, BlogId) VALUES(9, 'postCblog2', NULL, '2019-01-12', 6)
GO
INSERT INTO dbo.Post(Id, Title, Body, CreationDate, BlogId) VALUES(10, 'postDblog1', NULL, '2019-01-09', 7)
GO
INSERT INTO dbo.Post(Id, Title, Body, CreationDate, BlogId) VALUES(11, 'postABlog4', NULL, '2019-01-10', 2)
GO



  --
-- Заполним таблицу Category
--

  USE BizappsTest
 GO 
SET IDENTITY_INSERT Category ON
INSERT INTO dbo.Category(Id, CategoryName) VALUES(6, 'Базы данных')
GO
INSERT INTO dbo.Category(Id, CategoryName) VALUES(44, 'Веб программирование')
GO
INSERT INTO dbo.Category(Id, CategoryName) VALUES(22, 'Облачные технологии')
GO
INSERT INTO dbo.Category(Id, CategoryName) VALUES(1, 'Программирование')
GO
INSERT INTO dbo.Category(Id, CategoryName) VALUES(62, 'Разработка игр')
GO




  --
-- Заполним таблицу RelationPostCategory
--

    USE BizappsTest
 GO 
  SET IDENTITY_INSERT RelationPostCategory ON
INSERT INTO dbo.RelationPostCategory(PostId, CategoryId) VALUES(1, 6)
GO
INSERT INTO dbo.RelationPostCategory(PostId, CategoryId) VALUES(1, 44)
GO
INSERT INTO dbo.RelationPostCategory(PostId, CategoryId) VALUES(3, 22)
GO
INSERT INTO dbo.RelationPostCategory(PostId, CategoryId) VALUES(3, 62)
GO
INSERT INTO dbo.RelationPostCategory(PostId, CategoryId) VALUES(4, 62)
GO
INSERT INTO dbo.RelationPostCategory(PostId, CategoryId) VALUES(5, 62)
GO
INSERT INTO dbo.RelationPostCategory(PostId, CategoryId) VALUES(6, 1)
GO
INSERT INTO dbo.RelationPostCategory(PostId, CategoryId) VALUES(7, 44)
GO
INSERT INTO dbo.RelationPostCategory(PostId, CategoryId) VALUES(8, 22)
GO
INSERT INTO dbo.RelationPostCategory(PostId, CategoryId) VALUES(9, 6)
GO
INSERT INTO dbo.RelationPostCategory(PostId, CategoryId) VALUES(10, 1)
GO


