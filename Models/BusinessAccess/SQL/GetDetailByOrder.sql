SELECT [Id],[OrderId],[UserId],[UserName],[Item],[Price],[CreateDate]
  FROM [Ding].[dbo].[OrderDetail] WHERE IsEnable = 1 AND OrderId = @OrderId;