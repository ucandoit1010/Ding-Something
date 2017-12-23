SELECT [Id],[OrderNo],[OrderMemo],[PId],[PName],[CreateDate],[IsClose]
  FROM [dbo].[Orders] AS O  WHERE IsEnable = 1 ORDER BY CreateDate desc;