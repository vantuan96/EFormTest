declare @NumberRow INT;
declare @IndexRow INT;
set @NumberRow = (select count(C.Id) from IPDs  I
	join IPDCoronaryInterventions C on I.Id = C.VisitId
	where I.CreatedAt >= CONVERT(datetimeoffset(5), (select Value from AppConfigs where [Key] = 'UPDATE_VERSION2_A01_076_290422_VE'))
);
set @IndexRow = 1;

while @IndexRow <= @NumberRow
begin
	update IPDCoronaryInterventions set [Version] = 2
	where Id = (select Id from (select STT = ROW_NUMBER() over(order by C.Id), C.Id from IPDs  I
				join IPDCoronaryInterventions C on I.Id = C.VisitId
				where I.CreatedAt >= CONVERT(datetimeoffset(5), (select Value from AppConfigs where [Key] = 'UPDATE_VERSION2_A01_076_290422_VE'))) AS A
				where A.STT = @IndexRow)

	set @IndexRow = @IndexRow + 1;
end

update IPDCoronaryInterventions set [Version] = 1 where [Version] = 0

update ProcedureSummaryV2 set Version = '1'