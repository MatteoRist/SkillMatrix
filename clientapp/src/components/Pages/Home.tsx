import { Avatar, Box, Card, CardActionArea, CardContent, Grid, Typography, useTheme } from '@mui/material';
import { FC } from 'react';
import { Link } from 'react-router-dom';
import { tokens } from '../../theme';
import RadarChart from '../RadarChart/RadarChart';
import { AppLinksDict } from '../../constants';


// define props
interface HomeProps {
    avatar: string; // URL of the profile picture
    userName: string;
    userRole: string;
    // add any other props you need for the radar chart
}

const Home: FC<HomeProps> = ({ avatar, userName, userRole }) => {

    const theme = useTheme();
    const colors = tokens(theme.palette.mode);

    return (
        userName && userRole ? (
            <Grid container spacing={4} sx={{ p: 3 }}>
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
                <Grid item xs={6}>
                    <Card component={Link} to={AppLinksDict['skills'].path}>
                        <Box
                            bgcolor={colors.primary[400]}
                            display="flex"
                            justifyContent="center"
                            alignItems="stretch" sx={{ p: 3 }}
                        >
                            <RadarChart
                                labels={ ['Thing 1', 'Thing 2', 'Thing 3', 'Thing 4', 'Thing 5', 'Thing 6'] }
                                data={ [10, 50, 30, 50, 20, 80] }
                            />
                        </Box>
                    </Card>
                </Grid>

                {/* Surveys Portal */}
                <Grid item xs={6}>
                    <Card>
                        <CardActionArea component={Link} to={AppLinksDict['survey'].path} >
                            <CardContent>
                                <Box display="flex" flexDirection="column" alignItems="center">
                                    {
                                        AppLinksDict['survey'].icon
                                    }
                                    <Typography variant="h5" component="div" gutterBottom>
                                        {AppLinksDict['survey'].name}
                                    </Typography>
                                    <Typography variant="h6" color={colors.grey[100]}>
                                        Check out new surveys
                                    </Typography>
                                </Box>
                            </CardContent>
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



//import React, { useEffect, useState } from 'react';

//const apiUrl = 'https://localhost:7207/api/v1.0/' //process.env.REACT_APP_API_URL;

////console.log(process)
////console.log(process.env)
////console.log(process.env.REACT_APP_API_URL)

//const Home: React.FC = () => {
//    const [records, setRecords] = useState<Record[]>([])

//    useEffect(() => {
//        fetch(`${apiUrl}records`)
//            .then((response) => response.json())
//            .then((data) => {
//                // Check if response status is 4xx or 5xx
//                if (data.status >= 400 && data.status <= 599) {
//                    // create an error and reject it
//                    return Promise.reject(`HTTP Error: ${data.status}, details: ${data.details}`);
//                }
//                setRecords(data as Record[])
//            })
//            .catch((error) => console.error("Error:", error));
//    }, [])

//    return (
//        <main>
//            {
//               <div>Loading...{records.length}</div>
//            }
//        </main>
//    )
//}

//type User = {
//    userID: number;
//    name: string;
//    email: string;
//}

//type Skill = {
//    skilId: number;
//    title: string;
//    category: string;
//}

//type Question = {
//    questionId: number;
//    body: string;
//}

//type Record = {
//    recordId: number;
//    user: User;
//    skill: Skill;
//    question: Question;
//}


//export default Home;