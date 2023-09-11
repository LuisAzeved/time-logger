import * as React from "react";
import Projects from "./views/Projects";
import "./style.css";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterMoment } from "@mui/x-date-pickers/AdapterMoment";

export default function App() {
  const queryClient = new QueryClient();

  return (
    <QueryClientProvider client={queryClient}>
      <LocalizationProvider dateAdapter={AdapterMoment}>
        <header className="bg-gray-900 text-white flex items-center h-12 w-full">
          <div className="container mx-auto">
            <a className="navbar-brand" href="/">
              Timelogger
            </a>
          </div>
        </header>

        <main>
          <div className="container mx-auto">
            <Projects />
          </div>
        </main>
      </LocalizationProvider>
    </QueryClientProvider>
  );
}
