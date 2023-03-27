import { Box, Container } from '@chakra-ui/react'
import Footer from '../shared/footer'
import Header from '../shared/header'

function AppLayout({ children }) {
    return (
        <>
            <Box bg={'gray.100'}>
                <Container as="header" maxWidth="container.xl">
                    <Header />
                </Container>
            </Box>
            <Box>
                <Container as="main" maxWidth="container.xl">
                    {children}
                </Container>
            </Box>
            <Box bg={'gray.100'}>
                <Container as="footer" maxWidth="container.xl">
                    <Footer />
                </Container>
            </Box>
        </>
    )
}

export default AppLayout
