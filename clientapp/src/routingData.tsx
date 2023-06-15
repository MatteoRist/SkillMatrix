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
    { path: "/login", name: "Login", icon: null, directNavigation: false },
    { path: "/skills", name: "Skills", icon: <SchoolOutlinedIcon />, directNavigation: true },
    { path: "/surveys", name: "Surveys", icon: <DescriptionOutlinedIcon />, directNavigation: true },
    { path: "/singlesurvey", name: "SingleSurvey", icon: null, directNavigation: false },
    { path: "/profile", name: "Profile", icon: <AccountCircleOutlinedIcon />, directNavigation: true },
    { path: "/settings", name: "Settings", icon: <SettingsIcon />, directNavigation: true }
]

const AppLinksDict : { [key: string]: IAppLink } = AppLinks.reduce((acc, link) => {
    acc[link.name.toLowerCase()] = link;
    return acc;
}, {} as { [key: string]: IAppLink });

export type IAppLinks = typeof AppLinks
export { AppLinks, AppLinksDict }