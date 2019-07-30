declare @id int 
select @id = 1
while @id >=1 and @id <= 10000
begin
    insert into Datacenter.Patients values('John-' + convert(varchar(5), @id), 'Doe', '2000-01-01', '123456789', '06991234567', 'test@test.com')
    select @id = @id + 1
end