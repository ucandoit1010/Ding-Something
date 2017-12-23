SELECT [Id],[OrderNo],[OrderMemo],[PId],[PName],[CreateDate],[IsClose]
  FROM [dbo].[Orders] AS O  WHERE Id = @Id;