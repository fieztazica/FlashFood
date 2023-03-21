import { Box, Container } from '@chakra-ui/react'
import Footer from '../shared/footer'
import Header from '../shared/header'

function AppLayout({ children }) {
    return (
        <>
                <Container as="header">
                    <Header />
                </Container>
                <Container as="main">{children}</Container>
                <Container as="footer">
                    <Footer />
                </Container>
        </>
    )
}

export default AppLayout
