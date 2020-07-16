CREATE PROCEDURE [dbo].[spOrders_GetById]
	@Id int
AS
begin
	select nocount on;

	select * from dbo.[order]
	where Id = @Id;

end