import { Theme } from '@mui/material/styles';
import { CssBaseline, ThemeProvider } from "@mui/material";
import { ColorModeContext, useMode } from "./theme";
import { QueryClient, QueryClientProvider } from 'react-query';
import AppPages from './AppPages';

export default function App() {
    // set up theme from theme.tsx
    const [theme, colorMode]: [Theme, { toggleColorMode: () => void }] = useMode();

    // get data
    const queryClient = new QueryClient();

    return (
        <ColorModeContext.Provider value={colorMode}>
            <ThemeProvider theme={theme}>
                <CssBaseline />
                <QueryClientProvider client={queryClient}>
                    <AppPages />
                </QueryClientProvider>
            </ThemeProvider>
        </ColorModeContext.Provider>
    );
}