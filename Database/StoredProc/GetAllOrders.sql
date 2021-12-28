/*
Stored procedure to get the details of all orders
*/

CREATE or ALTER Procedure GetAllOrders
(
    @PageNumber AS INT,
    @RowsOfPage AS INT
)
AS 
BEGIN

SELECT 
CO.CreatedDate
,P.Name AS ProductName
,CO.Status AS OrderStatus
,OS.Status AS ShipmentStatus
,OS.Schedule AS ShipmentDate
from dbo.CustomerOrder CO WITH(NOLOCK)
inner join dbo.OrderShipment OS WITH(NOLOCK) ON OS.CustomerOrderId = CO.Id
inner join dbo.Product P WITH(NOLOCK) ON P.Id = CO.ProductId
order by CO.Id
OFFSET (@PageNumber-1)*@RowsOfPage ROWS
FETCH NEXT @RowsOfPage ROWS ONLY

END
GO

