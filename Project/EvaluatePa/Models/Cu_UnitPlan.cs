using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluatePa.Models
{
	public class Cu_UnitPlan
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Cu_Id { get; set; }
		public string Subject_Id { get; set; }
		public Int32 PlanDuration { get; set; }
		public string DateTime { get; set; }
		public string MDateTime { get; set; }
		public string Objective { get; set; }
		public string LearningStrands { get; set; }
		public string LearningStandards { get; set; }
		public string Indicators { get; set; }
		public string WorkLoad { get; set; }
		public string TeachingActivities { get; set; }
		public string TeachingResources { get; set; }
		public string Evaluate { get; set; }
		public Int32 OwnerId { get; set; }
		public Int32 Status { get; set; }
	}
}
