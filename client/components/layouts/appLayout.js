import { Box, Container } from '@chakra-ui/react'
import Footer from '../shared/footer'
import Header from '../shared/header'

function AppLayout({ children }) {
    return (
        <>
            <Box bg={'#eee'}>
                <Container as="header" maxWidth="container.xl">
                    <Header />
                </Container>
            </Box>
            <Box bg={'#fdf2e9'}>
                <Container as="main" maxWidth="container.xl">
                    {children}
                </Container>
            </Box>
            <Box bg={'#eee'}>
                <Container as="footer" maxWidth="container.xl">
                    <Footer />
                </Container>
            </Box>
        </>
    )
}

export default AppLayout
