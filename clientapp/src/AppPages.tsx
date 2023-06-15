import { Navigate, Route, Routes } from 'react-router-dom';
import Home from './components/Pages/Home';
import Login from './components/Pages/Login';
import NoPage from './components/Pages/NoPage';
import Surveys from './components/Pages/Surveys';
import Profile from './components/Pages/Profile';
import Skills from './components/Pages/Skills';
import SideNav from './components/SideNav/SideNav';
import { apiUrl, AppLinks } from './constants';
import avatar from './assets/pfp.jpg';
import { useMatrixApi } from './useMatrixApi';
import SingleSurvey from './components/Pages/SingleSurvey';

const AppPages = () => {

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

export default AppPages;