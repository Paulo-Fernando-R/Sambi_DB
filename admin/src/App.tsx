import AppRoutes from "./routes/AppRoutes"
import SideMenu from "./components/sideMenu/SideMenu"
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";

const queryClient = new QueryClient();
function App() {

  return (
    <>
      <QueryClientProvider client={queryClient}>
        <SideMenu />
        <AppRoutes />
      </QueryClientProvider>
    </>
  )
}

export default App
