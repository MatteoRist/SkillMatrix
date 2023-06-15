// Declaration of gloabls

declare interface User {
    userID: number;
    name: string;
    email: string;
}

declare interface Category {
    categoryId: number;
    name: string;
    skills: Skill[];
}

declare interface Skill {
    skillId: number;
    title: string;
}

declare interface Question {
    questionId: number;
    body: sstring;
    minValue: number;
    maxValue: number;
}