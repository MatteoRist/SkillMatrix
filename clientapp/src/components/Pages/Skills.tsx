import { Box, Card, Grid, Typography, useTheme } from "@mui/material";
import { Link } from 'react-router-dom';
import { AppLinksDict } from "../../constants";
import { tokens } from "../../theme";
import RadarChart from "../RadarChart/RadarChart";

const Skills = () => {

    const theme = useTheme();
    const colors = tokens(theme.palette.mode);

    return (
        <Grid container spacing={4} sx={{ p: 3 }}>
            <Grid item xs={12}>
                <Typography variant="h1">
                    {AppLinksDict['skills'].name}
                </Typography>
            </Grid>
            <Grid item xs={6}>
                <Card component={Link} to={AppLinksDict['skills'].path}>
                    <Box
                        bgcolor={colors.primary[400]}
                        display="flex"
                        justifyContent="center"
                        alignItems="stretch" sx={{ p: 3 }}
                    >
                        <RadarChart
                            labels={['Thing 1', 'Thing 2', 'Thing 3', 'Thing 4', 'Thing 5', 'Thing 6']}
                            data={[10, 50, 30, 50, 20, 80]}
                        />
                    </Box>
                </Card>
            </Grid>
        </Grid>
    )
}

export default Skills;