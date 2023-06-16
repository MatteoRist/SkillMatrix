import axios from 'axios';

export class MatrixApi {
    constructor(private baseUrl: string) { }

    // Fetch Users
    getUsers = () =>
        axios.get<User[]>(`${this.baseUrl}/users`).then((response) => response.data);

    // Fetch User
    getUser = (userId: number) =>
        axios.get<User>(`${this.baseUrl}/users/${userId}`).then((response) => response.data);

    // Fetch Categories with skills
    getSkillsAndCategories = () =>
        axios.get<Category[]>(`${this.baseUrl}/categories`).then((response) => response.data);

    // Add Skill
    addSkill = (newSkill: Skill) =>
        axios.post(`${this.baseUrl}/skills`, newSkill).then((response) => response.data);

    // Fetch Questions
    getQuestions = () =>
        axios.get<Question[]>(`${this.baseUrl}/questions`).then((response) => response.data);

    // Add Record
    addRecords = (records: MatrixRecord[]) =>
        axios.post(`${this.baseUrl}/records/bulk`, records).then((response) => response.data);

    // Fetch Statistics
    getStatistics = (userId: number) =>
        axios.get<Statistic[]>(`${this.baseUrl}/users/${userId}/statistics`).then((response) => response.data);

}
