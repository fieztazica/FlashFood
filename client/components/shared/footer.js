import {
    Box,
    Container,
    Link,
    SimpleGrid,
    Stack,
    Text,
    Flex,
    Tag,
    useColorModeValue,
    Grid,
    VStack,
} from '@chakra-ui/react'
import { ReactNode } from 'react'
import Logo from './logo'

const ListHeader = ({ children }) => {
    return (
        <Text fontWeight={'500'} fontSize={'lg'} mt={2}>
            {children}
        </Text>
    )
}

export default function LargeWithLogoCentered() {
    return (
        <Box py={10}>
            <Stack
                px={{ base: "0", md: "2" }}
                direction={{ base: "column", md: "row" }}
                align="left"
                display={{ base: "flex" }}
                justifyContent={"space-between"}
            >
                <VStack align="left">
                    <ListHeader>Product</ListHeader>
                    <Link href={'#'}>Overview</Link>
                    <Link href={'#'}>Pricing</Link>
                </VStack >
                <VStack align="left">
                    <ListHeader>Company</ListHeader>
                    <Link href={'#'}>About Us</Link>
                    <Link href={'#'}>Contact Us</Link>
                </VStack>
                <VStack align="left">
                    <ListHeader>Follow Us</ListHeader>
                    <Link href={'#'}>Facebook</Link>
                    <Link href={'#'}>Instagram</Link>
                </VStack>
            </Stack >

            <Flex
                flexDirection={"row"}
                mt={5}
                justifyContent={"space-between"}
                maxWidth="inherit"
            >
                <Logo />
                <Text alignSelf="center">
                    © {new Date().getFullYear()} FlashFood
                </Text>
            </Flex>
        </Box >
    )
}
