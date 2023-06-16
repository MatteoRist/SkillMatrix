import { Box, Card, Grid, Typography, useTheme } from "@mui/material";
import { FC } from "react";
import { Link } from 'react-router-dom';
import { AppLinksDict } from "../../routingData";
import { tokens } from "../../theme";
import RadarChart from "../RadarChart/RadarChart";

interface SkillsProps {
    chartLabels: string[];
    chartData: number[];
}

const Skills: FC<SkillsProps> = ({ chartLabels, chartData }) => {

    const theme = useTheme();
    const colors = tokens(theme.palette.mode);

    return (
        <Grid container spacing={4} >
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
                            labels={chartLabels}
                            data={chartData}
                        />
                    </Box>
                </Card>
            </Grid>
        </Grid>
    )
}

export default Skills;