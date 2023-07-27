import {
    Avatar,
    Box,
    ButtonBase,
    Card,
    CardActionArea,
    CardContent,
    Grid,
    IconButton,
    Typography,
    useTheme
} from '@mui/material';
import React, {FC, ReactElement} from 'react';
import { Link } from 'react-router-dom';
import { tokens } from '../../theme';
import { AppLinksDict } from '../../routingData';


// define props
interface HomeProps {
    avatar: string; // URL of the profile picture
    userName: string;
    userRole: string;
    chart: ReactElement
    // add any other props you need for the radar chart
}

const Home: FC<HomeProps> = ({ avatar, userName, userRole , chart}) => {

    const theme = useTheme();
    const colors = tokens(theme.palette.mode);

    return (
        userName && userRole ? (
            <Grid container spacing={4}>
                {/* Profile data section */}
                <Grid item xs={12} container alignItems="center" spacing={2}>
                    <Grid item component={Link} to={AppLinksDict['profile'].path} >
                        <Box display="flex"
                            justifyContent="center"
                            alignItems="center"
                        >
                            <Avatar src={avatar} sx={{ width: 100, height: 100 }} />
                        </Box>
                    </Grid>
                    <Grid item>
                        <Typography variant="h1">{userName}</Typography>
                        <Typography variant="h2">{userRole}</Typography>
                    </Grid>
                </Grid>

                {/* Radar Chart View */}
                <Grid item xs={5}>
                    <Card component={Link} to={AppLinksDict['skills'].path}>
                        <Box
                            bgcolor={colors.primary[400]}
                            display="flex"
                            justifyContent="center"
                            alignItems="stretch" sx={{ p: 3 }}
                        >
                            {chart}
                        </Box>
                    </Card>
                </Grid>

                {/* Surveys Portal */}

                <Grid item xs={6}>
                    <Card sx={{ maxWidth: 345 }}>
                        <CardActionArea component={Link} to={AppLinksDict['surveys'].path}>
                            <Box display="flex" flexDirection="column" alignItems="center">
                                <IconButton style={{ fontSize: 50 }}>
                                    {AppLinksDict['surveys'].icon}
                                </IconButton>
                                <CardContent>
                                        <Typography gutterBottom variant="h5" component="div" align="center">
                                            {AppLinksDict['surveys'].name}
                                        </Typography>
                                        <Typography variant="h6" component="div" align="center">
                                            Check out new surveys
                                        </Typography>
                                </CardContent>
                            </Box>
                    </CardActionArea>
                    </Card>
                </Grid>
            </Grid>
        ) : (
            <Typography variant="h1">LOADING...</Typography>
        )
    );
}

export default Home;