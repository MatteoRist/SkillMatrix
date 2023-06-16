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

export default function App() {
    // set up theme from theme.tsx
    const [theme, colorMode]: [Theme, { toggleColorMode: () => void }] = useMode();

    // instanziate matrixApi class using useMemo in order to cache the result and avoid possible infinite re-rendering
    const matrixApi = useMemo(() => new MatrixApi(apiUrl), []);

    // states
    const [selectedSkill, setSelectedSkill] = useState<Skill | null>(null);
    const [userData, setUserData] = useState<User | null>(null);
    const [skillsData, setSkillsData] = useState<Category[] | null>(null);
    const [questionsData, setQuestionsData] = useState<Question[] | null>(null);
    const [userStatisticsData, setUserStatisticsData] = useState<Statistic[] | null>(null);

    // fetch data from api using MatrixApi
    useEffect(() => {
        matrixApi.getUser(1).then(setUserData);
        matrixApi.getSkillsAndCategories().then(setSkillsData);
        matrixApi.getQuestions().then(setQuestionsData);
        matrixApi.getStatistics(1).then(setUserStatisticsData);
    }, [matrixApi]);

    // define handle to add records, useCallback to cash the function body and not evalueate it again on re-render
    const handleAddRecords = useCallback((questionIds: number[], answers: number[]) => {
        const matrixRecordList: MatrixRecord[] = answers.map((answer, index) => ({
            recordId: 0,
            userId: userData ? userData.userId : 0,
            skillId: selectedSkill ? selectedSkill.skillId : 0,
            questionId: questionIds[index],
            value: answer
        }));

        matrixApi.addRecords(matrixRecordList).then(() => {
            // TODO: Handle successful addition of records.
        }).catch(error => {
            // TODO: Handle errors.
            console.error('Error occurred while adding records:', error);
        });
    }, [matrixApi, userData, selectedSkill]);

    return (
        <ColorModeContext.Provider value={colorMode}>
            <ThemeProvider theme={theme}>
                <CssBaseline />
                <div className="app">
                    <SideNav navItems={AppLinks} avatar={avatar} />
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
                                        <Route path="/" index element={<Home avatar={avatar} userName={userData.name} userRole={userData.email} />} />
                                        <Route path="skills" element={<Skills chartLabels={userStatisticsData.map(stat => stat.categoryName)} chartData={userStatisticsData.map(stat => stat.statValue)} />} />
                                        <Route path="profile" element={<Profile />} />
                                        <Route path="surveys" element={<Surveys skills={skillsData} setSelectedSkill={setSelectedSkill} />} />
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