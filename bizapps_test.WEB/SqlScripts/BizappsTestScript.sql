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



CREATE TABLE [dbo].[Blog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BlogName] [varchar](40) NOT NULL,
	[CreationDate] [date] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Blog_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [KEY_Blog_UserId] UNIQUE NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Blog] ADD  DEFAULT (getdate()) FOR [CreationDate]
GO

ALTER TABLE [dbo].[Blog]  WITH CHECK ADD  CONSTRAINT [FK_Blog_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[BlogUser] ([Id])
GO

ALTER TABLE [dbo].[Blog] CHECK CONSTRAINT [FK_Blog_UserId]
GO





CREATE TABLE [dbo].[Post](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Body] [text] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[BlogId] [int] NOT NULL,
	[PostImage] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Post_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Post] ADD  DEFAULT (getdate()) FOR [CreationDate]
GO

ALTER TABLE [dbo].[Post]  WITH CHECK ADD  CONSTRAINT [FK_Post_BlogId] FOREIGN KEY([BlogId])
REFERENCES [dbo].[Blog] ([Id])
GO

ALTER TABLE [dbo].[Post] CHECK CONSTRAINT [FK_Post_BlogId]
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


CREATE TABLE [dbo].[UserRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](30) NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[RelationUserRole](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_RelationUserRole] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[RelationUserRole]  WITH CHECK ADD  CONSTRAINT [FK_RelationUserRole_BlogUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[BlogUser] ([Id])
GO

ALTER TABLE [dbo].[RelationUserRole] CHECK CONSTRAINT [FK_RelationUserRole_BlogUser]
GO

ALTER TABLE [dbo].[RelationUserRole]  WITH CHECK ADD  CONSTRAINT [FK_RelationUserRole_UserRole] FOREIGN KEY([RoleId])
REFERENCES [dbo].[UserRole] ([Id])
GO

ALTER TABLE [dbo].[RelationUserRole] CHECK CONSTRAINT [FK_RelationUserRole_UserRole]
GO


INSERT INTO [dbo].[BlogUser]
           ([UserName]
           ,[UserPassword])
     VALUES
           ('admin'
           ,'1111')
GO

INSERT INTO [dbo].[Blog]
           ([BlogName],
           [UserId])
     VALUES
           ('adminblog',
           IDENT_CURRENT('BlogUser'))
GO

INSERT INTO [dbo].[UserRole]
           ([RoleName])
     VALUES
           ('administrator')
GO

INSERT INTO [dbo].[RelationUserRole]
           ([UserId]
           ,[RoleId])
     VALUES
           (IDENT_CURRENT('BlogUser')
           ,IDENT_CURRENT('UserRole'))
GO




CREATE TABLE [dbo].[Comment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CommentText] [varchar](500) NOT NULL,
	[PostId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[ParentId] [int] NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Comment_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Comment] ADD  DEFAULT (getdate()) FOR [CreationDate]
GO

ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_ParentId] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Comment] ([Id])
GO

ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_ParentId]
GO

ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_PostId] FOREIGN KEY([PostId])
REFERENCES [dbo].[Post] ([Id])
GO

ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_PostId]
GO

ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[BlogUser] ([Id])
GO

ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_UserId]
GO





CREATE FUNCTION [dbo].[CompareUserLoginPassword] 
(	
	@UserName VARCHAR(50),
	@UserPassword VARCHAR(50)
)
RETURNS 
  @table TABLE 
(
  UserName VARCHAR(50)
)
AS
  BEGIN

    INSERT INTO @table
	  SELECT bu.UserName
	  FROM BlogUser bu
    WHERE bu.UserName =@UserName and bu.UserPassword=@UserPassword
	RETURN
  END
GO



CREATE FUNCTION [dbo].[GetAdminPermission] 
(	
	@UserName VARCHAR(50)
)
RETURNS 
  @table TABLE 
(
  UserId int
)
AS
  BEGIN

    INSERT INTO @table
	  SELECT rur.UserId
	  FROM RelationUserRole rur
    WHERE rur.UserId =(select Id from BlogUser where UserName=@UserName )
	and rur.RoleId = (select Id from UserRole where RoleName='administrator')
	RETURN
  END
GO


CREATE FUNCTION [dbo].[GetAllCategories]()
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

CREATE FUNCTION [dbo].[GetAllUserPosts](
  @UserId INT
  )
RETURNS 
  @table TABLE 
(
  PostId INT,
  Title VARCHAR(50),
  Body TEXT,
  PostImage VARCHAR(100)
)
AS
  BEGIN

    INSERT INTO @table
	  select Id, Title, Body, PostImage
	  from Post
    WHERE BlogId = (SELECT Id FROM Blog  WHERE UserId=@UserId)
	RETURN
  END

GO


CREATE FUNCTION [dbo].[GetAllUsers]()
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


CREATE FUNCTION [dbo].[GetBlogCategories] 
(	
	@UserName VARCHAR(50)
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
	  select c.Id, c.CategoryName
	  from Category c
	  join RelationBlogCategory rbc on rbc.CategoryId = c.Id
    WHERE rbc.BlogId = (SELECT Id FROM Blog  WHERE UserId=(SELECT Id FROM BlogUser where UserName = @UserName))
	RETURN
  END
GO




CREATE FUNCTION [dbo].[GetBlogUser](
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


CREATE FUNCTION [dbo].[GetBlogUserByName](
  @UserName VARCHAR(50)
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
    WHERE bu.UserName =@UserName
	RETURN
  END
GO






CREATE FUNCTION [dbo].[GetCategory](
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




CREATE FUNCTION [dbo].[GetComment] ( 
  @CommentId INT
  )
RETURNS 
  @table TABLE 
(
  CommentId INT,
  CommentText VARCHAR(500),
  UserName VARCHAR(50),
  CreationDate DATETIME,
  PostId INT
)
AS
  BEGIN

    INSERT INTO @table
	  select c.Id, c.CommentText, bu.UserName, c.CreationDate, c.PostId
	  from Comment c
    join Post p on p.Id = c.PostId
    join Blog b on b.Id = p.BlogId
    join BlogUser bu on bu.Id = b.UserId
    WHERE c.Id = @CommentId 
	RETURN
  END
GO






CREATE FUNCTION [dbo].[GetCommentAnswers] (
  @CommentId INT
  )
RETURNS 
  @table TABLE 
(
  CommentId INT,
  CommentText VARCHAR(500),
  UserName VARCHAR(50),
  CreationDate DATETIME,
  PostId INT
)
AS
  BEGIN

    INSERT INTO @table
	  select c.Id, c.CommentText, bu.UserName, c.CreationDate, c.PostId
	  from Comment c
    join BlogUser bu on bu.Id = c.UserId
    WHERE c.ParentId = @CommentId 
  	order by CreationDate desc
	RETURN
  END
GO








CREATE FUNCTION [dbo].[GetIndependentPostComments] (
  @PostId INT
  )
RETURNS 
  @table TABLE 
(
  CommentId INT,
  CommentText VARCHAR(500),
  UserName VARCHAR(50),
  CreationDate DATETIME,
  PostId INT
)
AS
  BEGIN

    INSERT INTO @table
	  select c.Id, c.CommentText, bu.UserName, c.CreationDate, c.PostId
	  from Comment c
    join BlogUser bu on bu.Id = c.UserId
    WHERE c.PostId = @PostId 
    and c.ParentId is null
  	order by CreationDate desc
	RETURN
  END
GO






CREATE FUNCTION [dbo].[GetPost](
  @PostId int
  )
RETURNS 
  @table TABLE 
(
  PostId INT,
  Title VARCHAR(50),
  Body TEXT,
  CreationDate DATE,
  PostImage VARCHAR(100)
)
AS
  BEGIN

    INSERT INTO @table
	  SELECT p.Id, p.Title, p.Body, p.CreationDate, p.PostImage
	  FROM Post p
    WHERE p.Id =@PostId
	RETURN
  END

GO



CREATE FUNCTION [dbo].[GetPostCategories](
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


CREATE FUNCTION [dbo].[GetUserPostsByName] 
(	
	 @UserName VARCHAR(50)
  )
RETURNS 
  @table TABLE 
(
  PostId INT,
  Title VARCHAR(50),
  Body TEXT,
  CreationDate DATETIME,
  PostImage VARCHAR(100)
)
AS
  BEGIN

    INSERT INTO @table
	  select Id, Title, Body, CreationDate, PostImage
	  from Post
    WHERE BlogId = (SELECT Id FROM Blog  WHERE UserId=(SELECT Id FROM BlogUser where UserName = @UserName))
	order by CreationDate desc
	RETURN
  END
GO


CREATE FUNCTION [dbo].[GetUserPostsByUserNameAndCategory] 
(	
	 @UserName VARCHAR(50),
	 @CategoryId INT
  )
RETURNS 
  @table TABLE 
(
  PostId INT,
  Title VARCHAR(50),
  Body TEXT,
  CreationDate DATETIME,
  PostImage VARCHAR(100)
)
AS
  BEGIN

    INSERT INTO @table
	  select Id, Title, Body, CreationDate, PostImage
	  from Post p
	  join RelationPostCategory rpc on rpc.PostId = p.Id
    WHERE BlogId = (SELECT Id FROM Blog  WHERE UserId=(SELECT Id FROM BlogUser where UserName = @UserName)) AND
	rpc.CategoryId = @CategoryId
	ORDER by CreationDate desc
	RETURN
  END
GO



CREATE FUNCTION [dbo].[GetUserPostsWithoutCategory] 
(	
		
	@UserName VARCHAR(50)
  )
RETURNS 
  @table TABLE 
(
  PostId INT,
  Title VARCHAR(50),
  Body TEXT,
  CreationDate DATETIME,
  PostImage VARCHAR(100)
)
AS
  BEGIN

    INSERT INTO @table
	  select Id, Title, Body, CreationDate, PostImage
	  from Post p
	  left join RelationPostCategory rpc on rpc.PostId = p.Id
    WHERE BlogId = (SELECT Id FROM Blog  WHERE UserId=(SELECT Id FROM BlogUser where UserName = @UserName)) AND
	rpc.PostId is null
	ORDER by CreationDate desc
	RETURN
  END


GO




CREATE PROCEDURE [dbo].[AddDeleteCategoryPost]
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





CREATE PROCEDURE [dbo].[IUDBlogUser]
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
                    UserId)
                    VALUES (
                    @BlogName,
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




CREATE PROCEDURE [dbo].[IUDCategory]
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
						    DELETE FROM RelationBlogCategory
							WHERE CategoryId=@CategoryId;

							DELETE FROM RelationPostCategory
							WHERE CategoryId=@CategoryId;

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




CREATE PROCEDURE [dbo].[IUDComment] (
 @Flag CHAR(1),
  @CommentId INT = null,
  @CommentText VARCHAR(500) = null,
  @PostId INT = null,
  @ParentId INT = null,
  @UserName VARCHAR(50) = null,
  @NewCommentId INT OUTPUT
)
AS 
BEGIN
  BEGIN TRY
    BEGIN TRAN;
 DECLARE @ErrorList VARCHAR(50) ='';


IF ((RTRIM(LTRIM(@Flag)))='I')
  BEGIN  
           IF ((@CommentText IS NULL)OR((RTRIM(LTRIM(@CommentText)))=''))
            BEGIN  
               IF (@ErrorList = '')
                  SET @ErrorList = 'Comment text not set';
               ELSE 
                  SET @ErrorList = @ErrorList +'Comment text not set';
              END
           IF ((@UserName IS NULL)OR((RTRIM(LTRIM(@UserName)))='')) 
            BEGIN  
               IF (@ErrorList = '')
                  SET @ErrorList = 'User name not set';
               ELSE 
                  SET @ErrorList = @ErrorList +'User name not set';
              END
            IF ((@PostId is not null) OR (@ParentId is not null)) 
            BEGIN  
                IF((@ParentId IS NOT NULL)AND(@ParentId!=0)) 
                  BEGIN
                      IF (@ErrorList = '')
                            BEGIN
                              INSERT INTO Comment(
                                CommentText,
                                PostId,
                                UserId,
					                      ParentId)
                               VALUES (
                                @CommentText,
                                (SELECT PostId from Comment where Id=@ParentId),
                                (SELECT Id FROM BlogUser WHERE UserName = @UserName),
					                      @ParentId);

                               SET @NewCommentId = IDENT_CURRENT('Comment') ;
  	                          END 
                  END
                  ELSE
                  BEGIN
                    IF((@PostId IS NOT NULL)AND(@PostId!=0))
                      BEGIN
                         IF (@ErrorList = '')
                            BEGIN
                              INSERT INTO Comment(
                                CommentText,
                                PostId,
                                UserId)
                               VALUES (
                                @CommentText,
                                @PostId,
                                (SELECT Id FROM BlogUser WHERE UserName = @UserName))

                               SET @NewCommentId = IDENT_CURRENT('Comment') ;
  	                          END 
                        END
                  END

              END
            ELSE
               BEGIN
                    IF (@ErrorList = '')
                     SET @ErrorList = 'ParentId and PostId are not set';
                    ELSE 
                      SET @ErrorList = @ErrorList +'ParentId and PostId are not set';
               END

  END
ELSE 
  BEGIN
    IF 	((RTRIM(LTRIM(@Flag)))='U')
      BEGIN 
              IF (@CommentId IS NULL) 
                BEGIN
                IF (@ErrorList = '')
                   SET @ErrorList = 'Comment id not set';
                ELSE
                   SET @ErrorList = @ErrorList + 'Comment id not set'; 
                END
            IF ((@CommentText IS NULL)OR((RTRIM(LTRIM(@CommentText)))='')) 
            BEGIN  
               IF (@ErrorList = '')
                  SET @ErrorList = 'Comment text not set';
               ELSE 
                  SET @ErrorList = @ErrorList +'Comment text not set';
              END
            IF (@ErrorList = '')
              BEGIN

                        UPDATE Comment 
                        SET CommentText = @CommentText
                        WHERE Id = @CommentId;
                        SET @NewCommentId  = @CommentId;
              END
      END
          ELSE
            BEGIN
              IF 	((RTRIM(LTRIM(@Flag)))='D')
                 BEGIN
                       IF (@CommentId IS NULL) 
                        BEGIN
                         IF (@ErrorList = '')
                            SET @ErrorList = 'Comment id not set';
                         ELSE
                            SET @ErrorList = @ErrorList + 'Comment id not set';
                        END

                      IF (@ErrorList = '')
                          BEGIN

                          DELETE 
                          FROM Comment  
                          WHERE ParentId = @CommentId;


                          DELETE 
                          FROM Comment  
                          WHERE Id = @CommentId;

                          SET @NewCommentId  = @CommentId;

                          END

                 END
             ELSE
                  RAISERROR ('Не верный флаг процедуры IUDComment', 18, 1) WITH NOWAIT;
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




CREATE PROCEDURE [dbo].[IUDPost]
(
  @Flag CHAR(1),
  @PostId INT,
  @UserId INT,
  @Title VARCHAR(50),
  @Body TEXT,
  @PostImage VARCHAR(100),
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
                  SET @ErrorList = 'User id not set';
               ELSE 
                  SET @ErrorList = @ErrorList +'User id not set';
              END
           IF ((@Title IS NULL)OR((RTRIM(LTRIM(@Title)))='')) 
            BEGIN  
               IF (@ErrorList = '')
                  SET @ErrorList = 'Post title not set';
               ELSE 
                  SET @ErrorList = @ErrorList +'Post title not set';
              END
            IF ((@PostImage IS NULL)OR((RTRIM(LTRIM(@PostImage)))='')) 
            BEGIN  
               IF (@ErrorList = '')
                  SET @ErrorList = 'Post image not set';
               ELSE 
                  SET @ErrorList = @ErrorList +'Post image not set';
              END
           IF (@ErrorList = '')
              BEGIN
                  INSERT INTO Post(
                      Title,
                      Body,
                      BlogId,
					  PostImage)
                  VALUES (
                      @Title,
                      @Body,
                      (SELECT Id FROM Blog WHERE UserId = @UserId),
					  @PostImage);

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
                   SET @ErrorList = 'Post id not set';
                ELSE
                   SET @ErrorList = @ErrorList + 'Post id not set'; 
                END
            IF ((@Title IS NULL)OR((RTRIM(LTRIM(@Title)))='')) 
            BEGIN  
               IF (@ErrorList = '')
                  SET @ErrorList = 'Post title not set';
               ELSE 
                  SET @ErrorList = @ErrorList +'Post title not set';
              END
            IF (@ErrorList = '')
              BEGIN
                  IF ((@PostImage IS NULL)OR((RTRIM(LTRIM(@PostImage)))=''))
                    BEGIN
                        UPDATE Post 
                        SET Title = @Title,
                            Body = @Body
                        WHERE Id = @PostId;
                        SET @NewPostId  = @PostId;
                    END
                  ELSE
                    BEGIN
                        UPDATE Post 
                        SET Title = @Title,
                            Body = @Body,
				                  	PostImage=@PostImage
                        WHERE Id = @PostId;
                        SET @NewPostId  = @PostId;
                    END
              END
      END
          ELSE
            BEGIN
              IF 	((RTRIM(LTRIM(@Flag)))='D')
                 BEGIN
                       IF (@PostId IS NULL) 
                        BEGIN
                         IF (@ErrorList = '')
                            SET @ErrorList = 'Post id not set';
                         ELSE
                            SET @ErrorList = @ErrorList + 'Post id not set';
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


CREATE PROCEDURE [dbo].[ChangePassword]
(
  @UserName VARCHAR(50) = null,
  @Password VARCHAR(50) = null,
  @UserId  INT OUTPUT 
)
AS 
BEGIN
  BEGIN TRY
    BEGIN TRAN;
 DECLARE @ErrorList VARCHAR(50) ='';

           IF ((@UserName IS NULL)OR(LTRIM(RTRIM(@UserName))=''))
            BEGIN  
               IF (@ErrorList = '')
                  SET @ErrorList = 'UserName is not set';
               ELSE 
                  SET @ErrorList = @ErrorList +'UserName is not set';
              END
          IF ((@Password IS NULL)OR(LTRIM(RTRIM(@Password))=''))
            BEGIN  
               IF (@ErrorList = '')
                  SET @ErrorList = 'Password is not set';
               ELSE 
                  SET @ErrorList = @ErrorList +'Password is not set';
              END

           IF (@ErrorList = '')
              BEGIN
                 UPDATE BlogUser
				 SET UserPassword = @Password
				 WHERE UserName = @UserName

				 SET @UserId = (SELECT Id FROM BlogUser WHERE UserName = @UserName);
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


/****** Object:  StoredProcedure [dbo].[CheckNestingLevel]    Script Date: 3/22/2019 3:15:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CheckNestingLevel]
(
  @CommentId INT,
  @HaveMoreThanFourLevels  INT OUTPUT 
)
AS 
BEGIN
  BEGIN TRY
 DECLARE @LastParentId INT;

 SET @LastParentId = (SELECT com3.ParentId 
                      FROM Comment com 
					  JOIN Comment com1 ON com.ParentId = com1.Id
					  JOIN Comment com2 ON com1.ParentId = com2.Id
					  JOIN Comment com3 ON com2.ParentId = com3.Id
					  WHERE com.Id = @CommentId);

 IF((@LastParentId is NULL)OR(@LastParentId=0))
 SET @HaveMoreThanFourLevels = 0;

 ELSE
 SET @HaveMoreThanFourLevels = 1;

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




