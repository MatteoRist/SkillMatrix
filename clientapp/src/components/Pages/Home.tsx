import { Box } from '@mui/material';
import React, { FC } from 'react';
import { Link } from 'react-router-dom';

// define props
interface HomeProps {
    avatar: string; // URL of the profile picture
    userName: string;
    userRole: string;
    // add any other props you need for the radar chart
}

const Home: FC<HomeProps> = ({ avatar, userName, userRole }) => {
    return (
        (userName && userRole) ?
            <div>

                {/* profile data section*/ }
                <Box className="avatarIcon">
                    <img
                        alt="profile-user"
                        width="100px"
                        height="100px"
                        src={avatar}
                    />
                    <div style={{ marginLeft: '10px' }}>
                        <h1>{userName}</h1>
                        <h2>{userRole}</h2>
                    </div>
                </Box>

                {/* Radar chart */}
                <div style={{ flex: '1', display: 'flex', justifyContent: 'flex-start' }}>
                    {/* Insert your radar chart here. I'm not familiar with Chart.js, so I've left this blank. */}
                </div>

                {/* Link to surveys */}
                <div style={{ flex: '1', display: 'flex', justifyContent: 'flex-end' }}>
                    <Link to="/surveys">Go to Surveys</Link>
                </div>

            </div> :
            <h1>LOADING...</h1>
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