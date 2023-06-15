import { useQuery, useMutation } from 'react-query';

export function useMatrixApi(baseUrl: string) {
    // Fetch Users
    const useGetUsers = () =>
        useQuery<User[]>('users', () =>
            fetch(`${baseUrl}/users`).then((response) => response.json())
        );

    // Fetch User
    const useGetUser = (userId: number) =>
        useQuery<User>('users', () =>
            fetch(`${baseUrl}/users/${userId}`).then((response) => response.json())
        );

    // Fetch Categories with skills
    const useGetSkillsAndCategories = () =>
        useQuery<Category[]>('categories', () =>
            fetch(`${baseUrl}/categories`).then((response) => response.json())
        );

    // Add Skill
    const useAddSkill = () =>
        useMutation((newSkill: Skill) =>
            fetch(`${baseUrl}/skills`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(newSkill),
            }).then((response) => response.json())
        );

    // Fetch Questions
    const useGetQuestions = () =>
        useQuery<Question[]>('questions', () =>
            fetch(`${baseUrl}/questions`).then((response) => response.json())
        );

    // Add methods for fetching and posting Questions, Records, and Category

    return {
        useGetUsers,
        useGetUser,
        useGetSkillsAndCategories,
        useAddSkill,
        useGetQuestions,
        // Add other methods here
    };
}
