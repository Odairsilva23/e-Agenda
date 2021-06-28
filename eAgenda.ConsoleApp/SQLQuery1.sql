insert into TBCompromisso
	(
		[Assunto],
		[Local],
		[DataAgendada], 
		[HoraInicio], 
		[HoraFim],
		[LinkWebConversa],
		[Id_Contatos]
		
	) 
	values 
	(
		'Reunião de Pauta',
		 'NDD',
		'07/08/2021',
		'14:30',
		'15:00',
		'DAsdgsyufgdayfgsdyfgdsyufgyfsgudfbsdfhdvbsfysvdsy',
		2
		
	)
	
	SELECT SCOPE_IDENTITY();
	SELECT [Id]
      ,[Assunto]
      ,[Local]
      ,[DataAgendada]
      ,[HoraInicio]
      ,[HoraFim]
      ,[LinkWebConversa]
      ,[Id_Contatos]
  FROM [dbo].[TBCompromisso]
   SELECT 
                C.[ID],       
                C.[ASSUNTO],       
                C.[LOCAL],             
                C.[DATAAGENDADA],                    
                C.[HORAINICIO], 
                C.[HORAFIM],
                C.[LINKWEBCONVERSA], 
                CO.[NOME] 
            FROM
                [TBCOMPROMISSO] C LEFT JOIN
                [TBCONTATOS] CO
            ON 
                C.ID_CONTATOS = CO.ID
             WHERE 
                C.[DATAAGENDADA] >  GETUTCDATE()
				ORDER BY 
				C.[DATAAGENDADA] ASC 
SELECT  * FROM [TBCOMPROMISSO] WHERE DATAAGENDADA > GETUTCDATE() ;