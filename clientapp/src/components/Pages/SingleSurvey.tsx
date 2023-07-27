import { Button, Radio, FormControlLabel, Slider, Typography, useTheme, Box, RadioGroup } from '@mui/material';
import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { AppLinksDict } from '../../routingData';
import { tokens } from '../../theme';

interface SingleSurveyProps {
    questions: Question[];
    sendRecords: (questionIds: number[], answers: number[]) => void;
}

const SingleSurvey: React.FC<SingleSurveyProps> = ({ questions, sendRecords }) => {
    const theme = useTheme();
    const colors = tokens(theme.palette.mode);

    const navigate = useNavigate();

    let { skill } = useParams();
    const [currentQuestion, setCurrentQuestion] = useState(0);
    const [questionIds, setQuestionIds] = useState<number[]>([]);
    const [answers, setAnswers] = useState<number[]>([]);
    const [selectedAnswer, setSelectedAnswer] = useState<number | null>(null);

    const submitSurvey = () => {
        sendRecords(questionIds, answers)
    }

    // useEffect in order to prevent un-necessary and potentially dangerouse submitSurvey calls every time a re-rendering happens
    useEffect(() => {
        if (currentQuestion >= questions.length) {
            submitSurvey();

            navigate(AppLinksDict['surveys'].path);
        }
    }, [currentQuestion, questions.length, navigate]);

    const question = questions[currentQuestion] || questions[0];    
    const optionsRange = question.maxValue - question.minValue + 1;

    const nextQuestion = (answer: number | null) => {
        if (answer) {
            setAnswers([...answers, answer]);
            setQuestionIds([...questionIds, question.questionId])
            setCurrentQuestion(currentQuestion + 1);
        }
    }

    return (
        <Box sx={{ m: 3 }}>
            <Typography variant="h4">{skill}</Typography>
            <Typography variant="body1">{question.body.replace('{}', skill || '')}</Typography>
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
