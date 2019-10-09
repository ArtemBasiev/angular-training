/*
***************************************************************************************
    Категории, в которых есть посты пользователя А, но нет постов пользователя Б
****************************************************************************************
     Исправлено для MS SQL
*****************************************************************************
*/
  USE BizappsTest
  GO

select cat.CategoryName
from Category cat
where cat.Id in (select rpc.CategoryId
from RelationPostCategory rpc
join Post p on p.Id =rpc.PostId
join Blog b on b.Id=p.BlogId
join BlogUser us  on us.Id=b.UserId

where  us.UserName like 'userA' and rpc.CategoryId not in  (select rpc1.CategoryId
from RelationPostCategory rpc1 
join Post pb on pb.Id =rpc1.PostId
join Blog blb on blb.Id=pb.BlogId
join BlogUser usb  on usb.Id=blb.UserId
where  usb.UserName like 'userB'
)
)
 GO
