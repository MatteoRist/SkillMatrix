import HomeOutlinedIcon from "@mui/icons-material/HomeOutlined";
import SettingsIcon from '@mui/icons-material/Settings';
import AccountCircleOutlinedIcon from '@mui/icons-material/AccountCircleOutlined';
import SchoolOutlinedIcon from '@mui/icons-material/SchoolOutlined';
import DescriptionOutlinedIcon from '@mui/icons-material/DescriptionOutlined';
import { ReactNode } from "react";


interface IAppLink {
    path: string;
    name: string;
    icon: ReactNode;
    directNavigation: boolean;
}


const AppLinks: IAppLink[] = [
    { path: "/", name: "Home", icon: <HomeOutlinedIcon />, directNavigation: true},
    { path: "/login", name: "Login", icon: <HomeOutlinedIcon />, directNavigation: false },
    { path: "/skills", name: "Skills", icon: <SchoolOutlinedIcon />, directNavigation: true },
    { path: "/survey", name: "Survey", icon: <DescriptionOutlinedIcon />, directNavigation: true },
    { path: "/profile", name: "Profile", icon: <AccountCircleOutlinedIcon />, directNavigation: true },
    { path: "/settings", name: "Settings", icon: <SettingsIcon />, directNavigation: true }
]

const apiUrl = 'https://localhost:7207/api/v1.0/' //process.env.REACT_APP_API_URL;

export type IAppLinks = typeof AppLinks
export { AppLinks, apiUrl }