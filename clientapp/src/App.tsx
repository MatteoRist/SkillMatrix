import { Route, Routes } from 'react-router-dom';
import Home from './components/Pages/Home';
import Login from './components/Pages/Login';
import NoPage from './components/Pages/NoPage';
import Survey from './components/Pages/Survey';
import Profile from './components/Pages/Profile';
import Skills from './components/Pages/Skills';
import { Theme } from '@mui/material/styles';
import { CssBaseline, ThemeProvider } from "@mui/material";
import { ColorModeContext, useMode } from "./theme";
import { useEffect, useState } from 'react';
import SideNav from './components/SideNav/SideNav';
import { apiUrl, AppLinks } from './constants';
import avatar from './assets/pfp.jpg'

interface User {
    userID: number;
    name: string;
    email: string;
}

export default function App() {
    // set up theme from theme.tsx
    const [theme, colorMode]: [Theme, { toggleColorMode: () => void }] = useMode();

    // get user data
    const [userData, setUserData] = useState<User | null>(null);
    const [isAuthenticated, setIsAuthenticated] = useState(false)

    // get users data
    useEffect(() => {
        fetch(`${apiUrl}users/1`)
            .then((response) => response.json())
            .then((data) => {
                // Check if response status is 4xx or 5xx
                if (data.status >= 400 && data.status <= 599) {
                    // create an error and reject it
                    return Promise.reject(`HTTP Error: ${data.status}, details: ${data.details}`);
                }
                setUserData(data as User)
                setIsAuthenticated(true)
            })
            .catch((error) => console.error("Error:", error));
    }, [])

    //const [profilePicture, setProfilePicture] = useState<string>('');

    //// fetch pfp
    //useEffect(() => {
    //    fetchProfilePicture();
    //}, []);

    //const fetchProfilePicture = async () => {
    //    try {
    //        const response = await fetch('profilePic');
    //        const blob = await response.blob();
    //        let a = URL.createObjectURL(blob)
    //        console.log(a)
    //        setProfilePicture(a);
    //    } catch (error) {
    //        console.error('Failed to fetch image', error);
    //    }
    //};

    return (
        <ColorModeContext.Provider value={colorMode}>
            <ThemeProvider theme={theme}>
                <CssBaseline />
                <div className="app">
                    <SideNav navItems={AppLinks} avatar={avatar} />
                    <main className="content">
                        <Routes>
                            <Route path="login" element={<Login />} />
                            {
                                isAuthenticated && userData && <>
                                    <Route path="/" index element={<Home avatar={avatar} userName={userData.name} userRole={userData.email} />} />
                                    <Route path="skills" element={<Skills />} />
                                    <Route path="profile" element={<Profile />} />
                                    <Route path="survey" element={<Survey />} />
                                    <Route path="*" element={<NoPage />} />
                                </>
                            }
                        </Routes>
                    </main>
                </div>
            </ThemeProvider>
        </ColorModeContext.Provider>
    );
}
