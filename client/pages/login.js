import Head from 'next/head'
import Image from 'next/image'
import AppLayout from '@/components/layouts/appLayout'
import { Box, Button, Checkbox, Flex, FormControl, FormErrorMessage, FormHelperText, FormLabel, Heading, Input, Link, Stack, Text, useColorModeValue, useToast } from '@chakra-ui/react'
import { useEffect, useState } from 'react'
import { useAppStates } from '../lib/AppContext'
import { useRouter } from 'next/router'
import { useFormik } from 'formik'
import { getReasonPhrase } from 'http-status-codes'
import NextLink from "next/link";

const initialValues = {
    email: '',
    password: '',
}

function Login() {
    const router = useRouter();
    const redirectTo = router.query?.["redirect"]
    const { api, user } = useAppStates();
    const toast = useToast();
    const [loading, setLoading] = useState(false);

    const formik = useFormik({
        initialValues,
        onSubmit: async (values) => {
            try {
                setLoading(true);
                await api.login(values);
                router.push(redirectTo ? `${decodeURIComponent(redirectTo)}` : `/`)
                toast({
                    title: 'Success!',
                    status: 'success',
                })
            } catch (e) {
                toast({
                    title: 'There is an error occured!',
                    description: `${getReasonPhrase(e.response.status)}`,
                    status: 'error',
                })
            } finally {
                setLoading(false);
            }
        },
    })

    useEffect(() => {
        if (!!user) {
            router.push(redirectTo ? `${decodeURIComponent(redirectTo)}` : `/`);
        }
    }, [user]);

    return (
        <>
            <Flex
                minH={'100vh'}
                align={'center'}
                justify={'center'}
                bg={useColorModeValue('gray.50', 'gray.800')}>
                <Stack spacing={8} mx={'auto'} maxW={'lg'} py={12} px={6}>
                    <Stack align={'center'}>
                        <Heading fontSize={'4xl'}>Sign in</Heading>
                        <Text fontSize={'lg'} color={'gray.600'}>
                            to enjoy all of our cool <Link color={'blue.400'}>features</Link> ??
                        </Text>
                    </Stack>
                    <form
                        onSubmit={formik.handleSubmit}>
                        <Box
                            rounded={'lg'}
                            bg={useColorModeValue('white', 'gray.700')}
                            boxShadow={'lg'}
                            p={8}
                            minW={'md'}
                        >
                            <Stack spacing={4}>
                                <FormControl id="email" isRequired>
                                    <FormLabel>Email address</FormLabel>
                                    <Input type="email" name="email" value={formik.values.email} onChange={formik.handleChange} />
                                </FormControl>
                                <FormControl id="password" isRequired>
                                    <FormLabel>Password</FormLabel>
                                    <Input type="password" name="password" value={formik.values.password} onChange={formik.handleChange} />
                                </FormControl>
                                <Stack spacing={10}>
                                    <Stack
                                        direction={{ base: 'column', sm: 'row' }}
                                        align={'start'}
                                        justify={'space-between'}>
                                        <Link as={NextLink} color={'blue.400'} href={redirectTo ? `/register?redirect=${redirectTo}` : `/register`}>Don&#39;t have an account?</Link>
                                        <Link color={'blue.400'}>Forgot password?</Link>
                                    </Stack>
                                    <Button
                                        isLoading={loading}
                                        type="submit"
                                        size="lg"
                                        bg={'blue.400'}
                                        color={'white'}
                                        _hover={{
                                            bg: 'blue.500',
                                        }}>
                                        Sign in
                                    </Button>
                                </Stack>
                            </Stack>
                        </Box>
                    </form>
                </Stack>
            </Flex>
        </>
    )
}

export default Login
