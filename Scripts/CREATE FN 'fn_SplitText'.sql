IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_SplitText]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_SplitText]
GO

/*

SELECT *
FROM [dbo].[fn_SplitText]('monday,tuesday,thursday', ',')

*/


CREATE FUNCTION [dbo].[fn_SplitText] (@Data VARCHAR(MAX), @delimeter CHAR(1))
RETURNS @v_Values TABLE ([Position] BIGINT IDENTITY(1,1), [TxtValue]  VARCHAR(MAX)) AS
BEGIN
   DECLARE @v_value VARCHAR(MAX),
           @v_pos BIGINT

   SET @v_Pos = 1
   WHILE @data > '' AND @v_Pos > 0
      BEGIN
         SELECT @v_pos = CHARINDEX(@delimeter, @data, 1), 
                @v_Value = CASE @v_Pos WHEN 0 THEN @data 
                                       ELSE LEFT(@data, @v_pos - 1)
                           END,
                @data = CASE WHEN @v_Value = @data THEN NULL 
                             ELSE SUBSTRING(@data, @v_pos + 1, LEN(@data)) 
                        END 
         INSERT INTO @v_Values ([TxtValue])
            VALUES (@v_Value)
      END
   RETURN
END

