using System.Collections.Generic;

namespace AntiGrade.Shared.InputModels
{
    public class WorkDto
    {
         public int Id {get;set;}
         public int Name {get;set;}
         public decimal Points {get;set;}
         public List<CriteriaDto> CriteriasDto {get;set;}
    }
}