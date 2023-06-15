import { Navigate, Route, Routes } from 'react-router-dom';
import Home from './components/Pages/Home';
import Login from './components/Pages/Login';
import NoPage from './components/Pages/NoPage';
import Surveys from './components/Pages/Surveys';
import Profile from './components/Pages/Profile';
import Skills from './components/Pages/Skills';
import { Theme } from '@mui/material/styles';
import { CssBaseline, ThemeProvider } from "@mui/material";
import { ColorModeContext, useMode } from "./theme";
import SideNav from './components/SideNav/SideNav';
import { apiUrl, AppLinks } from './constants';
import avatar from './assets/pfp.jpg'
import { QueryClient, QueryClientProvider } from 'react-query';
import { useMatrixApi } from './useMatrixApi';
import SingleSurvey from './components/Pages/SingleSurvey';

export default function App() {
    // set up theme from theme.tsx
    const [theme, colorMode]: [Theme, { toggleColorMode: () => void }] = useMode();

    // get data
    const queryClient = new QueryClient();

    return (
        <ColorModeContext.Provider value={colorMode}>
            <ThemeProvider theme={theme}>
                <CssBaseline />
                <QueryClientProvider client={queryClient}>
                    <Body />
                </QueryClientProvider>
            </ThemeProvider>
        </ColorModeContext.Provider>
    );
}

function Body() {

    const { useGetUser, useGetSkillsAndCategories, useGetQuestions } = useMatrixApi(apiUrl);

    const UserData = useGetUser(1).data
    const SkillsData = useGetSkillsAndCategories().data
    const questionsData = useGetQuestions().data;

    return (
        <div className="app">
            <SideNav navItems={AppLinks} avatar={avatar} />
            <main className="content">
                <Routes>
                    <Route path="login" element={<Login />} />
                    {
                        (UserData && SkillsData && questionsData) ?
                            <>
                                <Route path="/" index element={<Home avatar={avatar} userName={UserData.name} userRole={UserData.email} />} />
                                <Route path="skills" element={<Skills />} />
                                <Route path="profile" element={<Profile />} />
                                <Route path="surveys" element={<Surveys skills={SkillsData} />} />
                                <Route path="singlesurvey/:skill" element={<SingleSurvey questions={questionsData} />} />
                            </> :
                            <Route path="/" index element={<Navigate to="/" replace />}></Route>
                    }
                    <Route path="*" element={<NoPage />} />
                </Routes>
            </main>
        </div>
    )
}
