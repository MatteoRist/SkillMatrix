import { useTheme } from '@mui/material';
import {
    Chart as ChartJS,
    RadialLinearScale,
    PointElement,
    LineElement,
    Filler,
    Tooltip,
    Legend,
} from 'chart.js';
import { FC } from 'react';
import { Radar } from 'react-chartjs-2';
import { tokens } from '../../theme';

ChartJS.register(
    RadialLinearScale,
    PointElement,
    LineElement,
    Filler,
    Tooltip,
    Legend
);

interface RadarChartProps {
    labels: string[];
    data: number[];
}

const RadarChart: FC<RadarChartProps> = (props) => {

    const theme = useTheme();
    const colors = tokens(theme.palette.mode);

    const data = {
        labels: props.labels, //['Thing 1', 'Thing 2', 'Thing 3', 'Thing 4', 'Thing 5', 'Thing 6'],
        datasets: [
            {
                data: props.data, //[10, 50, 30, 50, 20, 80],
                backgroundColor: colors.greenAccent[800],
                borderColor: colors.greenAccent[300],
                borderWidth: 1,
            },
        ],
    };

    const options = {
        plugins: {
            legend: {
                display: false, // this displays or not the dataset name above the charte
            }
        },
        scales: {
            r: {
                ticks: {
                    display: false,  // this displays or not the labels of the scale
                },
                angleLines: {
                    display: true,   // this displays or not running from the center to the border
                    color: colors.grey[300],
                },
                suggestedMax: 100
            }
        }
    };

    return <Radar
        data={data}
        options={options}
    />
}


export default RadarChart