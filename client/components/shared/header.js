import { ReactNode } from 'react'
import {
    Box,
    Flex,
    Avatar,
    HStack,
    IconButton,
    Link,
    Button,
    Menu,
    MenuButton,
    MenuList,
    MenuItem,
    MenuDivider,
    useDisclosure,
    useColorModeValue,
    Stack,
    Spacer,
    Switch,
    Heading,
} from '@chakra-ui/react'
import { HamburgerIcon, CloseIcon } from '@chakra-ui/icons'
import react from 'react'
import { FiShoppingCart } from 'react-icons/fi'
import NextLink from 'next/link'
import NavLink from '../NavLink'

const navLinks = [
    {
        text: "Home",
        href: "/"
    },
    {
        text: "Account",
        href: "#"
    },
    {
        text: "Orders",
        href: "/orders"
    }
]

const StyledNavLink = ({ href, children }) => (
    <NavLink
        px={2}
        py={1}
        rounded={'md'}
        _hover={{
            textDecoration: 'none',
            bg: useColorModeValue('gray.200', 'gray.700'),
        }}
        href={href}
    >
        {children}
    </NavLink>
)

export default function Simple() {
    const { isOpen, onOpen, onClose } = useDisclosure()

    return (
        <>
            <Box padding={[0, 4.8]}>
                <Flex
                    h={16}
                    alignItems={'center'}
                    justifyContent={'space-between'}
                >
                    <IconButton
                        size={'md'}
                        icon={isOpen ? <CloseIcon /> : <HamburgerIcon />}
                        aria-label={'Open Menu'}
                        display={{ md: 'none' }}
                        onClick={isOpen ? onClose : onOpen}
                    />

                    <Box as={NextLink} href="/" color={'#333'} fontSize={30}>
                        <Heading>
                            FlashFood
                        </Heading>
                    </Box>
                    <Flex alignItems={'center'}>
                        <HStack
                            as={'nav'}
                            display={{ base: 'none', md: 'flex' }}
                            color={'#333'}
                            fontWeight={100}
                        >
                            {navLinks.map((navLink, i) => (
                                <StyledNavLink key={"navlink-" + i} href={`${navLink.href}`}>
                                    {navLink.text}
                                </StyledNavLink>
                            ))}
                            <StyledNavLink href={'/cart'}>
                                <FiShoppingCart size={32} />
                            </StyledNavLink>
                        </HStack>
                        <Box display={{ base: 'flex', md: 'none' }}>
                            <StyledNavLink
                                href={'/cart'}
                            >
                                <FiShoppingCart size={32} />
                            </StyledNavLink>
                        </Box>

                    </Flex>
                </Flex>
                {isOpen ? (
                    <Box pb={4} display={{ md: 'none' }}>
                        <Stack as={'nav'} spacing={4}>
                            {navLinks.map((navLink, i) => (
                                <StyledNavLink key={"mb-navlink-" + i} href={navLink.href}>{navLink.text}</StyledNavLink>
                            ))}
                        </Stack>
                    </Box>
                ) : null}
            </Box>
        </>
    )
}
