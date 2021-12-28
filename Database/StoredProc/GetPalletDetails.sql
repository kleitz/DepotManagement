/*
Stored procedure to get the details of Pallet
*/

CREATE or ALTER Procedure GetPalletDetails 
(
    @PageNumber AS INT,
    @RowsOfPage AS INT
)
AS 
BEGIN

SELECT 
P.Quantity
,PQ.MaxQuantity
,PT.Type AS ProductType
,N.Location AS NodeLocation
,P.Priority
from dbo.Pallet P WITH(NOLOCK)
inner join dbo.PalletQuantity PQ WITH(NOLOCK)  ON PQ.Id = P.PalletQuantityId
inner join dbo.ProductType PT WITH(NOLOCK) ON PT.Id = PQ.ProductTypeId
inner join dbo.LPN L WITH(NOLOCK) ON L.Id = P.LPNId
inner join dbo.Nodes N WITH(NOLOCK) ON N.Id = L.NodeId
order by P.Id
OFFSET (@PageNumber-1)*@RowsOfPage ROWS
FETCH NEXT @RowsOfPage ROWS ONLY

END
GO