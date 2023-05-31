using skill_matrix_api.Models.Skills;

namespace skill_matrix_api
{
    public class SkillsDataStore
    {
        public List<Skill> Skills { get; set; }
        public static SkillsDataStore Current { get; } = new SkillsDataStore();

        public SkillsDataStore()
        {
            //init dummy data
            Skills = new List<Skill>() 
            {
                new Skill() { SkillId=1, Category="programming language", Title="python"},
                new Skill() { SkillId=2, Category="programming language", Title="java"},
                new Skill() { SkillId=3, Category="natural language", Title="english"}
            };  
        }
    }
}
