// Declaration of gloabls

declare interface User {
    userId: number;
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
    body: string;
    minValue: number;
    maxValue: number;
}

declare interface MatrixRecord {
    recordId: number;
    userId: number;
    skillId: number;
    questionId: number;
    value: number;
}

declare interface Statistic {
    userId: number;
    cateogryId: number;
    categoryName: string;
    statValue: number;
}

