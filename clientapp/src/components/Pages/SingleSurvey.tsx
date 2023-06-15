import { Button, Radio, FormControlLabel, Slider, Typography, useTheme, Box, RadioGroup } from '@mui/material';
import { useState } from 'react';
import { useParams } from 'react-router-dom';
import { tokens } from '../../theme';

interface SingleSurveyProps {
    questions: Question[];
}

const SingleSurvey: React.FC<SingleSurveyProps> = ({ questions }) => {
    const theme = useTheme();
    const colors = tokens(theme.palette.mode);

    let { skill } = useParams();
    const [currentQuestion, setCurrentQuestion] = useState(0);
    const [answers, setAnswers] = useState<number[]>([]);
    const [selectedAnswer, setSelectedAnswer] = useState<number | null>(null);

    const nextQuestion = (answer: number | null) => {
        if (answer) {
            setAnswers([...answers, answer]);
            setCurrentQuestion(currentQuestion + 1);
        }
    }

    const submitSurvey = () => {
        // implement the function here
    }

    if (currentQuestion >= questions.length) {
        submitSurvey();
        return null;
    }

    const question = questions[currentQuestion];
    const optionsRange = question.maxValue - question.minValue + 1;

    return (
        <Box sx={{ m: 3 }}>
            <Typography variant="h4">{skill}</Typography>
            <Typography variant="body1">{question.body}</Typography>
            {
                (optionsRange > 5) ?
                    (
                        <Slider
                            min={question.minValue}
                            max={question.maxValue}
                            onChange={(event, value) => setSelectedAnswer(value as number)}
                            valueLabelDisplay="auto"
                        />
                    ) :
                    (
                        <RadioGroup value={selectedAnswer} onChange={(event) => setSelectedAnswer(Number(event.target.value))}>
                            {Array.from({ length: optionsRange }, (_, i) => i + question.minValue).map(option => (
                                <FormControlLabel
                                    key={option}
                                    value={option}
                                    control={<Radio />}
                                    label={option}
                                />
                            ))}
                        </RadioGroup>
                    )}
            <Button
                variant="contained"
                disabled={!selectedAnswer}
                //color={colors.primary[400]}
                onClick={() => {
                    nextQuestion(selectedAnswer)
                }}
            >
                Next Question
            </Button>
        </Box>
    );
}

export default SingleSurvey;
