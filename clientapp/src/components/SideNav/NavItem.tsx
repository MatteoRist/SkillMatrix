import { ReactNode } from 'react';
import { Link } from 'react-router-dom';
import { useTheme, Typography} from "@mui/material";
import { tokens } from "../../theme";
import { MenuItem } from 'react-pro-sidebar';

interface ItemProps {
    title: string;
    to: string;
    icon: ReactNode
    selected: string;
    setSelected: (value: string) => void; // Assuming setSelected updates a string value
}

const NavItem: React.FC<ItemProps> = ({ title, to, icon, selected, setSelected }) => {
    const theme = useTheme();
    const colors = tokens(theme.palette.mode);
    return (
        <MenuItem
        
            active={selected === title}
            style={{
                color: colors.grey[100],
            }}
            onClick={() => setSelected(title)}
            icon={icon}
        >
            <Typography>{title}</Typography>
            <Link to={to} />
        </MenuItem>
    );
};

export default NavItem