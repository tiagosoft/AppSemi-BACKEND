--Ordenes creadas hoy
    SELECT 
        o.Id AS OrderId,
        o.PatientId,
        p.FullName,
        o.CreatedAt,
        COUNT(oe.ExamId) ExamCount,
        o.AttentionDate,
        STRING_AGG(e.Name, ', ') AS Exams
    FROM Orders o
    INNER JOIN Patients p ON o.PatientId = p.Id
    INNER JOIN OrderExams oe ON oe.OrderId = o.Id
    INNER JOIN Exams e ON e.Id = oe.ExamId
    WHERE o.CreatedAt = GETDATE()
    GROUP BY o.Id, o.PatientId, p.FullName,  o.CreatedAt,o.AttentionDate
    ORDER BY o.Id;

    --examenes de una orden especifica

        SELECT 
        o.Id AS OrderId,
        STRING_AGG(e.Name, ', ') AS Exams
    FROM Orders o
    INNER JOIN OrderExams oe ON oe.OrderId = o.Id
    INNER JOIN Exams e ON e.Id = oe.ExamId
    WHERE o.Id = 1
    GROUP BY o.Id
    ORDER BY o.Id;

    --ordenes de cada paciente

        SELECT DISTINCT
         p.Id,
          p.FullName,
        COUNT(o.Id) OrderCount     
    FROM Orders o
    INNER JOIN Patients p ON o.PatientId = p.Id
    GROUP BY p.Id, p.FullName
    ORDER BY p.Id;
