USE BizappsTest
GO

IF DB_NAME() <> N'BizappsTest' SET NOEXEC ON
GO

--
-- Создать таблицу [dbo].[BlogUser]
--
PRINT (N'Создать таблицу [dbo].[BlogUser]')
GO
CREATE TABLE dbo.BlogUser (
  Id int IDENTITY,
  UserName varchar(30) NOT NULL,
  UserPassword varchar(20) NOT NULL,
  CONSTRAINT PK_User_ID PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO


USE BizappsTest
GO

IF DB_NAME() <> N'BizappsTest' SET NOEXEC ON
GO

--
-- Создать таблицу [dbo].[Blog]
--
PRINT (N'Создать таблицу [dbo].[Blog]')
GO
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
PRINT (N'Создать внешний ключ [FK_Blog_UserId] для объекта типа таблица [dbo].[Blog]')
GO
ALTER TABLE dbo.Blog
  ADD CONSTRAINT FK_Blog_UserId FOREIGN KEY (UserId) REFERENCES dbo.BlogUser (Id)
GO

USE BizappsTest
GO

IF DB_NAME() <> N'BizappsTest' SET NOEXEC ON
GO

--
-- Создать таблицу [dbo].[Category]
--
PRINT (N'Создать таблицу [dbo].[Category]')
GO
CREATE TABLE dbo.Category (
  Id int IDENTITY,
  CategoryName varchar(50) NOT NULL,
  CONSTRAINT PK_Category_Id PRIMARY KEY CLUSTERED (Id),
  CONSTRAINT KEY_Category_CategoryName UNIQUE (CategoryName)
)
ON [PRIMARY]
GO

USE BizappsTest
GO

IF DB_NAME() <> N'BizappsTest' SET NOEXEC ON
GO

--
-- Создать таблицу [dbo].[Post]
--
PRINT (N'Создать таблицу [dbo].[Post]')
GO
CREATE TABLE dbo.Post (
  Id int IDENTITY,
  Title varchar(50) NOT NULL,
  Body text NOT NULL,
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
PRINT (N'Создать внешний ключ [FK_Post_BlogId] для объекта типа таблица [dbo].[Post]')
GO
ALTER TABLE dbo.Post
  ADD CONSTRAINT FK_Post_BlogId FOREIGN KEY (BlogId) REFERENCES dbo.Blog (Id)
GO



USE BizappsTest
GO

IF DB_NAME() <> N'BizappsTest' SET NOEXEC ON
GO

--
-- Создать таблицу [dbo].[RelationBlogCategory]
--
PRINT (N'Создать таблицу [dbo].[RelationBlogCategory]')
GO
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
PRINT (N'Создать внешний ключ [FK_RelationBlogCategory_BlogId] для объекта типа таблица [dbo].[RelationBlogCategory]')
GO
ALTER TABLE dbo.RelationBlogCategory
  ADD CONSTRAINT FK_RelationBlogCategory_BlogId FOREIGN KEY (BlogId) REFERENCES dbo.Blog (Id)
GO

--
-- Создать внешний ключ [FK_RelationBlogCategory_CategoryId] для объекта типа таблица [dbo].[RelationBlogCategory]
--
PRINT (N'Создать внешний ключ [FK_RelationBlogCategory_CategoryId] для объекта типа таблица [dbo].[RelationBlogCategory]')
GO
ALTER TABLE dbo.RelationBlogCategory
  ADD CONSTRAINT FK_RelationBlogCategory_CategoryId FOREIGN KEY (CategoryId) REFERENCES dbo.Category (Id)
GO

USE BizappsTest
GO

IF DB_NAME() <> N'BizappsTest' SET NOEXEC ON
GO

--
-- Создать таблицу [dbo].[RelationPostCategory]
--
PRINT (N'Создать таблицу [dbo].[RelationPostCategory]')
GO
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
PRINT (N'Создать внешний ключ [FK_RelationPostCategory_CategoryId] для объекта типа таблица [dbo].[RelationPostCategory]')
GO
ALTER TABLE dbo.RelationPostCategory
  ADD CONSTRAINT FK_RelationPostCategory_CategoryId FOREIGN KEY (CategoryId) REFERENCES dbo.Category (Id)
GO

--
-- Создать внешний ключ [FK_RelationPostCategory_PostId] для объекта типа таблица [dbo].[RelationPostCategory]
--
PRINT (N'Создать внешний ключ [FK_RelationPostCategory_PostId] для объекта типа таблица [dbo].[RelationPostCategory]')
GO
ALTER TABLE dbo.RelationPostCategory
  ADD CONSTRAINT FK_RelationPostCategory_PostId FOREIGN KEY (PostId) REFERENCES dbo.Post (Id)
GO


USE BizappsTest
GO

IF DB_NAME() <> N'BizappsTest' SET NOEXEC ON
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

--
-- Создать процедуру [dbo].[AddDeleteCategoryPost]
--
GO
PRINT (N'Создать процедуру [dbo].[AddDeleteCategoryPost]')
GO

CREATE PROCEDURE dbo.AddDeleteCategoryPost
(
  @Flag CHAR(1),
  @PostId INT,
  @CategoryId INT
)
AS 
BEGIN
  BEGIN TRY
    BEGIN TRAN;
 DECLARE @ErrorList VARCHAR(50) ='';

   IF ((RTRIM(LTRIM(@Flag)))='I')
     BEGIN 
           IF (@PostId IS NULL)
            BEGIN  
               IF (@ErrorList = '')
                  SET @ErrorList = 'Не указан идентификатор поста';
               ELSE 
                  SET @ErrorList = @ErrorList +'Не указан идентификатор поста';
              END
          IF (@CategoryId IS NULL)
            BEGIN  
               IF (@ErrorList = '')
                  SET @ErrorList = 'Не указан идентификатор категории';
               ELSE 
                  SET @ErrorList = @ErrorList +'Не указан идентификатор категории';
              END

           IF ((@ErrorList = '') AND ((SELECT PostId FROM RelationPostCategory WHERE PostId=@PostId AND CategoryId=@CategoryId) IS NULL))
              BEGIN
               IF((SELECT BlogId FROM RelationBlogCategory rbc WHERE rbc.CategoryId=@CategoryId)IS NULL)
                BEGIN
                INSERT INTO RelationBlogCategory(
                  BlogId,
                  CategoryId)
                VALUES (
                (SELECT BlogId FROM Post p WHERE p.Id=@PostId),
                @CategoryId);
                END

                  INSERT INTO RelationPostCategory(
                      PostId,
                      CategoryId)
                  VALUES (
                      @PostId,
                      @CategoryId);
  	           END
END
  ELSE
    BEGIN
       IF ((RTRIM(LTRIM(@Flag)))='D')
           BEGIN
           IF (@PostId IS NULL)
            BEGIN  
               IF (@ErrorList = '')
                  SET @ErrorList = 'Не указан идентификатор поста';
               ELSE 
                  SET @ErrorList = @ErrorList +'Не указан идентификатор поста';
              END
          IF (@CategoryId IS NULL)
            BEGIN  
               IF (@ErrorList = '')
                  SET @ErrorList = 'Не указан идентификатор категории';
               ELSE 
                  SET @ErrorList = @ErrorList +'Не указан идентификатор категории';
              END
               IF((SELECT COUNT(*) FROM RelationPostCategory rpc 
                                JOIN Post p ON p.Id = rpc.PostId
                                WHERE  rpc.CategoryId=@CategoryId AND p.BlogId = (SELECT BlogId FROM Post WHERE Id=@PostId))<=1)
                                    BEGIN
                                      DELETE 
                                      FROM RelationBlogCategory  
                                      WHERE BlogId = (SELECT BlogId FROM Post WHERE Id=@PostId)
                                      AND CategoryId = @CategoryId;
                                    END 
                 DELETE 
                  FROM RelationPostCategory  
                   WHERE PostId = @PostId
                   AND CategoryId = @CategoryId;
           END
        ELSE
        RAISERROR ('Не верный флаг процедуры AddCategoryToPost', 18, 1) WITH NOWAIT;
    END
 IF ((RTRIM(LTRIM(@ErrorList))!=''))
      RAISERROR (@ErrorList, 18, 1) WITH NOWAIT;
COMMIT TRAN;
END TRY
BEGIN CATCH
   ROLLBACK TRAN;
      DECLARE @ErrorMessage NVARCHAR(4000);
      DECLARE @ErrorSeverity INT;
      DECLARE @ErrorState INT;

       SELECT
        @ErrorMessage = ERROR_MESSAGE(),
        @ErrorSeverity = ERROR_SEVERITY(),
        @ErrorState = ERROR_STATE();

      -- Use RAISERROR inside the CATCH block to return 
      -- error information about the original error that 
      -- caused execution to jump to the CATCH block.
      RAISERROR (@ErrorMessage, -- Message text.
      @ErrorSeverity, -- Severity.
      @ErrorState -- State.
      );
END CATCH
  END

  
GO

USE BizappsTest
GO

IF DB_NAME() <> N'BizappsTest' SET NOEXEC ON
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

--
-- Создать процедуру [dbo].[IUDBlogUser]
--
GO
PRINT (N'Создать процедуру [dbo].[IUDBlogUser]')
GO

CREATE PROCEDURE dbo.IUDBlogUser
(
  @Flag CHAR(1),
  @UserId INT,
  @UserName VARCHAR(50),
  @UserPassword VARCHAR(50),
  @BlogName VARCHAR(50),
  @NewUserId INT OUTPUT
  
)
AS 
BEGIN
  BEGIN TRY
    BEGIN TRAN;
 DECLARE @ErrorList VARCHAR(50) ='';


IF ((RTRIM(LTRIM(@Flag)))='I')
  BEGIN  
           IF ((@UserName IS NULL)OR((RTRIM(LTRIM(@UserName)))='')) 
            BEGIN  
               IF (@ErrorList = '')
                  SET @ErrorList = 'Не указан логин пользователя';
               ELSE 
                  SET @ErrorList = @ErrorList +'Не указан логин пользователя';
              END
          IF ((SELECT UserName FROM BlogUser WHERE UserName=(RTRIM(LTRIM(@UserName)))) IS NOT NULL)
          BEGIN
              IF (@ErrorList = '')
                  SET @ErrorList = 'Имя пользователя уже зарегистрировано';
               ELSE 
                  SET @ErrorList = @ErrorList +'Имя пользователя уже зарегистрировано';
          END
          IF ((@UserPassword IS NULL)OR((RTRIM(LTRIM(@UserPassword)))='')) 
            BEGIN  
               IF (@ErrorList = '')
                  SET @ErrorList = 'Не указан пароль';
               ELSE 
                  SET @ErrorList = @ErrorList +'Не указан пароль';
              END
           IF ((@BlogName IS NULL)OR((RTRIM(LTRIM(@BlogName)))='')) 
            BEGIN  
               IF (@ErrorList = '')
                  SET @ErrorList = 'Не указано название блога';
               ELSE 
                  SET @ErrorList = @ErrorList +'Не указано название блога';
              END
           IF (@ErrorList = '')
              BEGIN
                  INSERT INTO BlogUser(
                      UserName,
                      UserPassword)
                  VALUES (
                      @UserName,
                      @UserPassword);
                  INSERT INTO Blog(
                    BlogName,
                    CreationDate,
                    UserId)
                    VALUES (
                    @BlogName,
                    GETDATE(),
                    @@IDENTITY)

                    SET @NewUserId =IDENT_CURRENT('BlogUser');

  	           END

  END
ELSE 
  BEGIN
    IF 	((RTRIM(LTRIM(@Flag)))='U')
      BEGIN 
              IF (@UserId IS NULL) 
                BEGIN
                IF (@ErrorList = '')
                   SET @ErrorList = 'Не указан идентификатор пользователя';
                ELSE
                   SET @ErrorList = @ErrorList + 'Не указан идентификатор пользователя'; 
                END
            IF ((@UserName IS NULL)OR((RTRIM(LTRIM(@UserName)))='')) 
            BEGIN  
               IF (@ErrorList = '')
                  SET @ErrorList = 'Не указан логин пользователя';
               ELSE 
                  SET @ErrorList = @ErrorList +'Не указан логин пользователя';
              END
          IF ((SELECT UserName FROM BlogUser WHERE UserName=(RTRIM(LTRIM(@UserName)))) IS NOT NULL)
          BEGIN
              IF (@ErrorList = '')
                  SET @ErrorList = 'Имя пользователя уже зарегистрировано';
               ELSE 
                  SET @ErrorList = @ErrorList +'Имя пользователя уже зарегистрировано';
          END
           IF ((@BlogName IS NULL)OR((RTRIM(LTRIM(@BlogName)))='')) 
            BEGIN  
               IF (@ErrorList = '')
                  SET @ErrorList = 'Не указано название блога';
               ELSE 
                  SET @ErrorList = @ErrorList +'Не указано название блога';
              END
            IF (@ErrorList = '')
              BEGIN
                UPDATE BlogUser 
                SET UserName = @UserName,
                  UserPassword = @UserPassword
                WHERE Id = @UserId;

                UPDATE Blog 
                SET BlogName = @BlogName
                WHERE UserId = @UserId;

                SET @NewUserId = @UserId;
              END
      END
          ELSE
            BEGIN
              IF 	((RTRIM(LTRIM(@Flag)))='D')
                 BEGIN
                       IF (@UserId IS NULL) 
                        BEGIN
                         IF (@ErrorList = '')
                            SET @ErrorList = 'Не указан идентификатор пользователя';
                         ELSE
                            SET @ErrorList = @ErrorList + 'Не указан идентификатор пользователя';
                        END

                      IF (@ErrorList = '')
                          BEGIN
                            DELETE 
                            FROM RelationPostCategory  
                            WHERE PostId IN (SELECT Id FROM Post WHERE BlogId=(SELECT Id FROM Blog WHERE UserId=@UserId));

                            DELETE 
                            FROM RelationBlogCategory  
                            WHERE BlogId =(SELECT Id FROM Blog WHERE UserId=@UserId);

                            DELETE 
                            FROM Post  
                            WHERE BlogId=(SELECT Id FROM Blog WHERE UserId=@UserId);
                              
                            DELETE 
                            FROM Blog 
                            WHERE UserId =@UserId;

                            DELETE 
                            FROM BlogUser 
                            WHERE Id =@UserId;

                            SET @NewUserId = @UserId;

                          END

                 END
             ELSE
                  RAISERROR ('Не верный флаг процедуры IUDBlogUser', 18, 1) WITH NOWAIT;
            END
    
  END

 IF ((RTRIM(LTRIM(@ErrorList))!=''))
      RAISERROR (@ErrorList, 18, 1) WITH NOWAIT;
COMMIT TRAN;
END TRY
BEGIN CATCH
   ROLLBACK TRAN;
      DECLARE @ErrorMessage NVARCHAR(4000);
      DECLARE @ErrorSeverity INT;
      DECLARE @ErrorState INT;

       SELECT
        @ErrorMessage = ERROR_MESSAGE(),
        @ErrorSeverity = ERROR_SEVERITY(),
        @ErrorState = ERROR_STATE();

      -- Use RAISERROR inside the CATCH block to return 
      -- error information about the original error that 
      -- caused execution to jump to the CATCH block.
      RAISERROR (@ErrorMessage, -- Message text.
      @ErrorSeverity, -- Severity.
      @ErrorState -- State.
      );
END CATCH
  END

  
GO


USE BizappsTest
GO

IF DB_NAME() <> N'BizappsTest' SET NOEXEC ON
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

--
-- Создать процедуру [dbo].[IUDCategory]
--
GO
PRINT (N'Создать процедуру [dbo].[IUDCategory]')
GO
CREATE PROCEDURE dbo.IUDCategory
(
  @Flag CHAR(1),
  @CategoryId INT,
  @CategoryName VARCHAR(50),
  @NewCategoryId INT OUTPUT
  
)
AS 
BEGIN
  BEGIN TRY
    BEGIN TRAN;
 DECLARE @ErrorList VARCHAR(50) ='';


IF ((RTRIM(LTRIM(@Flag)))='I')
  BEGIN  
           IF ((@CategoryName IS NULL)OR((RTRIM(LTRIM(@CategoryName)))='')) 
            BEGIN  
               IF (@ErrorList = '')
                  SET @ErrorList = 'Не указано наименование категории';
               ELSE 
                  SET @ErrorList = @ErrorList +'Не указано наименование категории';
              END
          IF ((SELECT CategoryName FROM Category WHERE CategoryName=(RTRIM(LTRIM(@CategoryName)))) IS NOT NULL)
          BEGIN
              IF (@ErrorList = '')
                  SET @ErrorList = 'Категория уже существует';
               ELSE 
                  SET @ErrorList = @ErrorList +'Категория уже существует';
          END
           IF (@ErrorList = '')
              BEGIN
                  INSERT INTO Category(
                      CategoryName)
                  VALUES (
                      @CategoryName);

                  SET @NewCategoryId = IDENT_CURRENT('Category');
  	           END
  END
ELSE 
  BEGIN
    IF 	((RTRIM(LTRIM(@Flag)))='U')
      BEGIN  
            IF (@CategoryId IS NULL) 
                IF (@ErrorList = '')
                   SET @ErrorList = 'Не указан идентификатор категории';
                ELSE
                   SET @ErrorList = @ErrorList + 'Не указан идентификатор категории';
            IF ((@CategoryName IS NULL)OR((RTRIM(LTRIM(@CategoryName)))=''))   
               IF (@ErrorList = '')
                  SET @ErrorList = 'Не указано наименование категории';
               ELSE 
                  SET @ErrorList = @ErrorList +'Не указано наименование категории';
            IF (@ErrorList = '')
              BEGIN
                      UPDATE Category 
                SET CategoryName = @CategoryName
                WHERE Id = @CategoryId;
                SET @NewCategoryId = @CategoryId;
              END
      END
          ELSE
            BEGIN
              IF 	((RTRIM(LTRIM(@Flag)))='D')
                 BEGIN
                       IF (@CategoryId IS NULL) 
                         IF (@ErrorList = '')
                            SET @ErrorList = 'Не указан идентификатор категории';
                         ELSE
                            SET @ErrorList = @ErrorList + 'Не указан идентификатор категории';
                        IF (@ErrorList = '')
                          BEGIN
                             DELETE FROM Category 
                             WHERE Id = @CategoryId;

                             SET @NewCategoryId = @CategoryId;
                            END
                 END
             ELSE
                  RAISERROR ('Не верный флаг процедуры IUDCategory', 18, 1) WITH NOWAIT;
            END
    
  END

 IF ((RTRIM(LTRIM(@ErrorList))!=''))
      RAISERROR (@ErrorList, 18, 1) WITH NOWAIT;
COMMIT TRAN;
END TRY
BEGIN CATCH
   ROLLBACK TRAN;
      DECLARE @ErrorMessage NVARCHAR(4000);
      DECLARE @ErrorSeverity INT;
      DECLARE @ErrorState INT;

       SELECT
        @ErrorMessage = ERROR_MESSAGE(),
        @ErrorSeverity = ERROR_SEVERITY(),
        @ErrorState = ERROR_STATE();

      -- Use RAISERROR inside the CATCH block to return 
      -- error information about the original error that 
      -- caused execution to jump to the CATCH block.
      RAISERROR (@ErrorMessage, -- Message text.
      @ErrorSeverity, -- Severity.
      @ErrorState -- State.
      );
END CATCH
  END

  
GO




USE BizappsTest
GO

IF DB_NAME() <> N'BizappsTest' SET NOEXEC ON
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

--
-- Создать процедуру [dbo].[IUDPost]
--
GO
PRINT (N'Создать процедуру [dbo].[IUDPost]')
GO

CREATE PROCEDURE dbo.IUDPost
(
  @Flag CHAR(1),
  @PostId INT,
  @UserId INT,
  @Title VARCHAR(50),
  @Body TEXT,
  @NewPostId INT OUTPUT
)
AS 
BEGIN
  BEGIN TRY
    BEGIN TRAN;
 DECLARE @ErrorList VARCHAR(50) ='';


IF ((RTRIM(LTRIM(@Flag)))='I')
  BEGIN  
           IF (@UserId IS NULL)
            BEGIN  
               IF (@ErrorList = '')
                  SET @ErrorList = 'Не указан идентификатор пользователя';
               ELSE 
                  SET @ErrorList = @ErrorList +'Не указан идентификатор пользователя';
              END
           IF ((@Title IS NULL)OR((RTRIM(LTRIM(@Title)))='')) 
            BEGIN  
               IF (@ErrorList = '')
                  SET @ErrorList = 'Не указан заголовок поста';
               ELSE 
                  SET @ErrorList = @ErrorList +'Не указан заголовок поста';
              END
           IF (@ErrorList = '')
              BEGIN
                  INSERT INTO Post(
                      Title,
                      Body,
                      CreationDate,
                      BlogId)
                  VALUES (
                      @Title,
                      @Body,
                      GETDATE(),
                      (SELECT Id FROM Blog WHERE UserId = @UserId));

                  SET @NewPostId = IDENT_CURRENT('Post') ;
  	           END

  END
ELSE 
  BEGIN
    IF 	((RTRIM(LTRIM(@Flag)))='U')
      BEGIN 
              IF (@PostId IS NULL) 
                BEGIN
                IF (@ErrorList = '')
                   SET @ErrorList = 'Не указан идентификатор поста';
                ELSE
                   SET @ErrorList = @ErrorList + 'Не указан идентификатор поста'; 
                END
            IF ((@Title IS NULL)OR((RTRIM(LTRIM(@Title)))='')) 
            BEGIN  
               IF (@ErrorList = '')
                  SET @ErrorList = 'Не указан заголовок поста';
               ELSE 
                  SET @ErrorList = @ErrorList +'Не указан заголовок поста';
              END
            IF (@ErrorList = '')
              BEGIN

                UPDATE Post 
                SET Title = @Title,
                    Body = @Body
                WHERE Id = @PostId;
                SET @NewPostId  = @PostId;

              END
      END
          ELSE
            BEGIN
              IF 	((RTRIM(LTRIM(@Flag)))='D')
                 BEGIN
                       IF (@PostId IS NULL) 
                        BEGIN
                         IF (@ErrorList = '')
                            SET @ErrorList = 'Не указан идентификатор поста';
                         ELSE
                            SET @ErrorList = @ErrorList + 'Не указан идентификатор поста';
                        END

                      IF (@ErrorList = '')
                          BEGIN
                        DECLARE @CurCategory CURSOR
                        DECLARE @PostIdCur INT
                        DECLARE @CategoryIdCur INT

                            SET @CurCategory = CURSOR LOCAL FAST_FORWARD
                              FOR
                              SELECT  *
                              FROM RelationPostCategory rpc
                              WHERE rpc.PostId=@PostId

           
                           OPEN @CurCategory;
                         
                            FETCH NEXT FROM @CurCategory
                            INTO @PostIdCur, @CategoryIdCur;   
          
                             WHILE @@FETCH_STATUS = 0 
                             BEGIN 
                                IF((SELECT COUNT(*) FROM RelationPostCategory rpc 
                                JOIN Post p ON p.Id = rpc.PostId
                                WHERE  rpc.CategoryId=@CategoryIdCur AND p.BlogId = (SELECT BlogId FROM Post WHERE Id=@PostId))<=1)
                                    BEGIN
                                      DELETE 
                                      FROM RelationBlogCategory  
                                      WHERE BlogId = (SELECT BlogId FROM Post WHERE Id=@PostId)
                                      AND CategoryId = @CategoryIdCur;
                                    END  
                                 FETCH NEXT FROM @CurCategory INTO @PostIdCur, @CategoryIdCur;            
                              END
                           

                         CLOSE @CurCategory;
                         DEALLOCATE @CurCategory;

                          DELETE 
                          FROM RelationPostCategory  
                          WHERE PostId =@PostId;

                          DELETE 
                          FROM Post
                          WHERE Id =@PostId;

                          SET @NewPostId  = @PostId;

                          END

                 END
             ELSE
                  RAISERROR ('Не верный флаг процедуры IUDPost', 18, 1) WITH NOWAIT;
            END
    
  END

 IF ((RTRIM(LTRIM(@ErrorList))!=''))
      RAISERROR (@ErrorList, 18, 1) WITH NOWAIT;
COMMIT TRAN;
END TRY
BEGIN CATCH
   ROLLBACK TRAN;
      DECLARE @ErrorMessage NVARCHAR(4000);
      DECLARE @ErrorSeverity INT;
      DECLARE @ErrorState INT;

       SELECT
        @ErrorMessage = ERROR_MESSAGE(),
        @ErrorSeverity = ERROR_SEVERITY(),
        @ErrorState = ERROR_STATE();

      -- Use RAISERROR inside the CATCH block to return 
      -- error information about the original error that 
      -- caused execution to jump to the CATCH block.
      RAISERROR (@ErrorMessage, -- Message text.
      @ErrorSeverity, -- Severity.
      @ErrorState -- State.
      );
END CATCH
  END

  
GO


USE BizappsTest
GO

IF DB_NAME() <> N'BizappsTest' SET NOEXEC ON
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

--
-- Создать функцию [dbo].[GetAllCategories]
--
GO
PRINT (N'Создать функцию [dbo].[GetAllCategories]')
GO
CREATE FUNCTION dbo.GetAllCategories()
RETURNS 
  @table TABLE 
(
  CategoryId INT,
  CategoryName VARCHAR(50)
)
AS
  BEGIN

    INSERT INTO @table
	  select Id, CategoryName
	  from Category
	RETURN
  END

GO

USE BizappsTest
GO

IF DB_NAME() <> N'BizappsTest' SET NOEXEC ON
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

--
-- Создать функцию [dbo].[GetAllUserPosts]
--
GO
PRINT (N'Создать функцию [dbo].[GetAllUserPosts]')
GO
CREATE FUNCTION dbo.GetAllUserPosts(
  @UserId INT
  )
RETURNS 
  @table TABLE 
(
  PostId INT,
  Title VARCHAR(50),
  Body TEXT
)
AS
  BEGIN

    INSERT INTO @table
	  select Id, Title, Body
	  from Post
    WHERE BlogId = (SELECT Id FROM Blog  WHERE UserId=@UserId)
	RETURN
  END

GO


USE BizappsTest
GO

IF DB_NAME() <> N'BizappsTest' SET NOEXEC ON
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

--
-- Создать функцию [dbo].[GetAllUsers]
--
GO
PRINT (N'Создать функцию [dbo].[GetAllUsers]')
GO
CREATE FUNCTION dbo.GetAllUsers()
RETURNS 
  @table TABLE 
(
  UserId INT,
  UserName VARCHAR(50)
)
AS
  BEGIN

    INSERT INTO @table
	  select Id, UserName
	  from BlogUser
	RETURN
  END

GO


USE BizappsTest
GO

IF DB_NAME() <> N'BizappsTest' SET NOEXEC ON
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

--
-- Создать функцию [dbo].[GetBlogUser]
--
GO
PRINT (N'Создать функцию [dbo].[GetBlogUser]')
GO
CREATE FUNCTION dbo.GetBlogUser(
  @UserId INT
  )
RETURNS 
  @table TABLE 
(
  UserId INT,
  UserName VARCHAR(50),
  UserPassword VARCHAR(50),
  BlogName VARCHAR(50)
)
AS
  BEGIN

    INSERT INTO @table
	  SELECT bu.Id, bu.UserName, bu.UserPassword, b.BlogName
	  FROM BlogUser bu
    JOIN Blog b ON b.UserId=bu.Id
    WHERE bu.Id =@UserId
	RETURN
  END

GO


USE BizappsTest
GO

IF DB_NAME() <> N'BizappsTest' SET NOEXEC ON
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

--
-- Создать функцию [dbo].[GetCategory]
--
GO
PRINT (N'Создать функцию [dbo].[GetCategory]')
GO
CREATE FUNCTION dbo.GetCategory(
  @CategoryId int
  )
RETURNS 
  @table TABLE 
(
  CategoryId INT,
  CategoryName VARCHAR(50)
)
AS
  BEGIN

    INSERT INTO @table
	  SELECT Id, CategoryName
	  FROM Category
    WHERE Id =@CategoryId
	RETURN
  END

GO



USE BizappsTest
GO

IF DB_NAME() <> N'BizappsTest' SET NOEXEC ON
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

--
-- Создать функцию [dbo].[GetPost]
--
GO
PRINT (N'Создать функцию [dbo].[GetPost]')
GO
CREATE FUNCTION dbo.GetPost(
  @PostId int
  )
RETURNS 
  @table TABLE 
(
  PostId INT,
  Title VARCHAR(50),
  Body TEXT
)
AS
  BEGIN

    INSERT INTO @table
	  SELECT p.Id, p.Title, p.Body
	  FROM Post p
    WHERE p.Id =@PostId
	RETURN
  END

GO


USE BizappsTest
GO

IF DB_NAME() <> N'BizappsTest' SET NOEXEC ON
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

--
-- Создать функцию [dbo].[GetPostCategories]
--
GO
PRINT (N'Создать функцию [dbo].[GetPostCategories]')
GO
CREATE FUNCTION dbo.GetPostCategories(
  @PostId INT
  )
RETURNS 
  @table TABLE 
(
  CategoryId INT,
  CategoryName VARCHAR(50)
)
AS
  BEGIN

    INSERT INTO @table
	  select Id, CategoryName
	  from Category c
    JOIN RelationPostCategory rpc ON c.Id = rpc.CategoryId
    WHERE rpc.PostId = @PostId
	RETURN
  END

GO