using System.Collections.Generic;

namespace AntiGrade.Shared.InputModels
{
    public class WorkDto
    {
         public int Id {get;set;}
         public string Name {get;set;}
         public decimal MaxPoints {get;set;}
         public List<CriteriaDto> Criterias{get;set;}
    }
}