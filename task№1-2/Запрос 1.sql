/*
*********************************************************
    ТОП 10 активных пользователей за последний месяц
*********************************************************
      Исправлено для MS SQL
*********************************************************
*/
USE BizappsTest
GO 

select TOP 10  us.UserName
from BlogUser  us
 join   Blog  bl on bl.UserId = us.Id
 join Post p on p.BlogId = bl.Id
 where   p.CreationDate between '2019-01-01' and '2019-01-31'
 group by  us.UserName
 order by count(p.Id) DESC

GO