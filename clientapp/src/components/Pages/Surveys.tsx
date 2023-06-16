import { Box, Button, Card, CardActionArea, CardContent, Grid, Typography } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { AppLinksDict } from "../../routingData";

interface SurveyProps {
    skills: Category[];
    setSelectedSkill: (skill: Skill) => void;
}

const Surveys: React.FC<SurveyProps> = ({ skills, setSelectedSkill }) => {

    const navigate = useNavigate();

    return (
        <Box sx={{ m: 2 }}>
            {skills.map((category) => (
                <div key={category.categoryId}>
                    <Typography variant="h2">{category.name}</Typography>
                    <Grid container spacing={3}>
                        {category.skills.map((skill) => (
                            <Grid item xs={12} sm={6} md={4} lg={3} key={skill.skillId}>
                                <Card>
                                    <CardActionArea
                                        component={Button}
                                        onClick={() => {
                                            setSelectedSkill(skill)
                                            navigate(`${AppLinksDict['singlesurvey'].path}/${encodeURIComponent(skill.title)}`)
                                        }}
                                    >
                                        <CardContent>
                                            <Box display="flex" flexDirection="column" alignItems="center">
                                                <Typography variant="h3" component="div">
                                                    {skill.title}
                                                </Typography>
                                            </Box>
                                        </CardContent>
                                    </CardActionArea>
                                </Card>
                            </Grid>
                        ))}
                    </Grid>
                </div>
            ))}
        </Box>
    );
};

export default Surveys;