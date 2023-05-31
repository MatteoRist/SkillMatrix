using skill_matrix_api.Models.SkillLevels;
using skill_matrix_api.Models.SkillRecords;
using skill_matrix_api.Models.Skills;
using skill_matrix_api.Models.Users;
using System;

namespace skill_matrix_api
{
    public static class DataMapper
    {
        public static UserDto MapToDto(User user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                SkillRecords = null //user.SkillRecords.Select(MapToDto).ToList(),
            };
        }
        
        public static List<UserDto> MapToDto(List<User> users)
        {
            List<UserDto> response = new List<UserDto>();

            foreach (var user in users) { 
                response.Add(MapToDto(user));
            }

            return response;
        }

        public static SkillDto MapToDto(Skill skill)
        {
            return new SkillDto
            {
                SkillId = skill.SkillId,
                Category = skill.Category,
                Title = skill.Title,
            };
        }

        public static List<SkillDto> MapToDto(List<Skill> skills)
        {
            List<SkillDto> response = new List<SkillDto>();

            foreach (var skill in skills)
            {
                response.Add(MapToDto(skill));
            }

            return response;
        }

        public static SkillLevelDto MapToDto(SkillLevel skillLevel)
        {
            return new SkillLevelDto
            {
                LevelId = skillLevel.LevelId,
                Level = skillLevel.Level
            };
        }

        public static List<SkillLevelDto> MapToDto(List<SkillLevel> skillLevels)
        {
            List<SkillLevelDto> response = new List<SkillLevelDto>();

            foreach (var skillLevel in skillLevels)
            {
                response.Add(MapToDto(skillLevel));
            }

            return response;
        }

        public static SkillRecordDto MapToDto(SkillRecord skillRecord)
        {
            return new SkillRecordDto
            {
                RecordId = skillRecord.RecordId,
                UserId = skillRecord.UserId,
                SkillId = skillRecord.SkillId,
                LevelId = skillRecord.LevelId,
                YearsOfExperience = skillRecord.YearsOfExperience,
                Note = skillRecord.Note
            };
        }

        public static List<SkillRecordDto> MapToDto(List<SkillRecord> skillRecords) 
        { 
            List<SkillRecordDto> response = new List<SkillRecordDto>();

            foreach (var skillRecord in skillRecords)
            {
                response.Add(MapToDto(skillRecord));
            }

            return response;
        }
    }
}
