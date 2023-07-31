import { Navigate, Route, Routes } from 'react-router-dom';
import Home from './components/Pages/Home';
import Login from './components/Pages/Login';
import NoPage from './components/Pages/NoPage';
import Surveys from './components/Pages/Surveys';
import Profile from './components/Pages/Profile';
import Skills from './components/Pages/Skills';
import SideNav from './components/SideNav/SideNav';
import { AppLinks } from './routingData';
import avatar from './assets/pfp.jpg';
import { MatrixApi } from './MatrixApi';
import SingleSurvey from './components/Pages/SingleSurvey';
import { apiUrl } from './constants'
import { Box, CssBaseline, Theme, ThemeProvider } from '@mui/material';
import { useCallback, useEffect, useMemo, useState } from 'react';
import { ColorModeContext, useMode } from './theme';
import RadarChart from "./components/RadarChart/RadarChart";
import {Category} from "@mui/icons-material";

export default function App() {
    // set up theme from theme.tsx
    const [theme, colorMode]: [Theme, { toggleColorMode: () => void }] = useMode();

    // instanziate matrixApi class using useMemo in order to cache the result and avoid possible infinite re-rendering
    const matrixApi = useMemo(() => new MatrixApi(apiUrl), []);

    // states
    const [selectedSkill, setSelectedSkill] = useState<Skill | null>(null);
    const [userData, setUserData] = useState<User | null>(null);
    const [skillsData, setSkillsData] = useState<Category[] | []>([]);
    const [filteredSkillsData, setFilteredSkillsData] = useState<Category[] | []>([]);
    const [questionsData, setQuestionsData] = useState<Question[] | []>([]);
    const [recordsData, setRecordsData] = useState<MatrixRecord[] | []>([]);
    const [userStatisticsData, setUserStatisticsData] = useState<Statistic[] | []>([]);

    // fetch data from api using MatrixApi
    let fetchData = () => {
        // fetch user data
        let userDataPromise = matrixApi.getUser(1).catch(console.log);

        // fetch questions
        let QuestionsDataPromise = matrixApi.getQuestions().catch(console.log);

        // fetch skills
        let skillsDataPromise = matrixApi.getSkillsAndCategories().catch(console.log);

        // fetch user statistics
        let statisticsDataPromise = matrixApi.getStatistics(1).catch(console.log);

        Promise.all([
            userDataPromise,
            QuestionsDataPromise,
            skillsDataPromise,
            statisticsDataPromise
        ]).then((values: any[]) => {

            let [userData, questionsData, skillsData, userStatisticsData] = values

            setUserData(userData);
            setQuestionsData(questionsData);
            setSkillsData(skillsData);
            setUserStatisticsData(userStatisticsData);

            matrixApi.getRecords(userData?.userId || 0)
                .then((recordsData) =>{

                    setRecordsData(recordsData)

                    matrixApi.getRecords(userData?.userId || 0).then(setRecordsData).catch(console.log);

                    const countOccurrences = (arr: any[], val: any) => arr.reduce((a, v) => (v === val ? a + 1 : a), 0);

                    let recordedSkills = recordsData.map(record => record.skillId);

                    let filteredCategories = []

                    for (const category of skillsData) {

                        let filteredSkills = []

                        for (const skill of category.skills) {
                            //console.log(recordedSkills)
                            //console.log(skill.skillId)
                            if (countOccurrences(recordedSkills, skill.skillId) < questionsData.length){

                                filteredSkills.push(skill)
                            }
                        }

                        if (filteredSkills.length){

                            category.skills = filteredSkills
                            filteredCategories.push(category)
                        }
                    }

                    setFilteredSkillsData(filteredCategories);
                })
                .catch(console.log);
        })

    };

    //componentDidMount
    useEffect(() => {
        fetchData();
    }, []);

    // define handle to add records, useCallback to cash the function body and not evaluate it again on re-render
    const handleAddRecords = useCallback((questionIds: number[], answers: number[]) => {
        const matrixRecordList: MatrixRecord[] = answers.map((answer, index) => ({
            recordId: 0,
            userId: userData ? userData.userId : 0,
            skillId: selectedSkill ? selectedSkill.skillId : 0,
            questionId: questionIds[index],
            value: answer
        }));

        matrixApi.addRecords(matrixRecordList).then(() => {

            fetchData();
        }).catch(error => {

            console.error('Error occurred while adding records:', error);
        });
    }, [matrixApi, userData, selectedSkill]);

    return (
        <ColorModeContext.Provider value={colorMode}>
            <ThemeProvider theme={theme}>
                <CssBaseline />
                <div className="app">
                    <SideNav navItems={AppLinks} avatar={avatar} name={userData?.name || ''} role={userData?.email || ''}/>
                    <Box
                        component="main"
                        className="content"
                        sx={{ p: 3 }}
                    >
                        <Routes>
                            <Route path="login" element={<Login />} />
                            {
                                (userData && skillsData && questionsData && userStatisticsData) ?
                                    <>
                                        <Route path="/" index element={<Home avatar={avatar} userName={userData.name} userRole={userData.email} chart={<RadarChart
                                            labels={userStatisticsData.map(stat => stat.categoryName)}
                                            data={userStatisticsData.map(stat => stat.statValue)}
                                        />} />} />
                                        <Route path="skills" element={<Skills chart={<RadarChart
                                            labels={userStatisticsData.map(stat => stat.categoryName)}
                                            data={userStatisticsData.map(stat => stat.statValue)}
                                        />}/>}/>
                                        <Route path="profile" element={<Profile />} />
                                        <Route path="surveys" element={<Surveys skills={filteredSkillsData} setSelectedSkill={setSelectedSkill} />} />
                                        <Route path="singlesurvey/:skill" element={<SingleSurvey questions={questionsData} sendRecords={handleAddRecords} />} />
                                    </> :
                                    <Route path="/" index element={<Navigate to="/" replace />}></Route>
                            }
                            <Route path="*" element={<NoPage />} />
                        </Routes>
                    </Box>
                </div>
            </ThemeProvider>
        </ColorModeContext.Provider>
    );
}