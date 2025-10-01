truncate table SEM_DETAILS;
truncate table SEM_DETAILS_TERMINE;

EXEC CreateExceptionTable @StartDate = '2026-01-01', @EndDate = '2026-12-31'
EXEC CreateCourses_VZ @StartDate = '2026-01-01', @EndDate = '2026-12-31'

select * from SEM_DETAILS d
inner join SEM_DETAILS_TERMINE dt on d.SEDETID = dt.SEDETID
inner join SEM_Kursdaten kd on kd.SEID = d.SEID
