import { Box, Card, CardActionArea, CardContent, Grid, Typography } from "@mui/material";
import { Link } from 'react-router-dom';
import { AppLinksDict } from "../../constants";

interface SurveyProps {
    skills: Category[];
}

const Surveys: React.FC<SurveyProps> = ({ skills }) => {
    return (
        <div>
            {skills.map((category) => (
                <div key={category.categoryId}>
                    <Typography variant="h2">{category.name}</Typography>
                    <Grid container spacing={3}>
                        {category.skills.map((skill) => (
                            <Grid item xs={12} sm={6} md={4} lg={3} key={skill.skillId}>
                                <Card>
                                    <CardActionArea component={Link} to={`${AppLinksDict['singlesurvey'].path}/${skill.title}`} >
                                        <CardContent>
                                            <Box display="flex" flexDirection="column" alignItems="center">
                                                <Typography variant="h3" component="div">
                                                    { skill.title }
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
        </div>
    );
};

export default Surveys;